using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebShop.Models;
using WebShop.Repositories;
using LinqKit;

namespace WebShop.Services
{
    /// <summary>
    /// Provides product services to controllers.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly UnitOfWork _uow;

        public ProductService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<Product> GetAllProducts()
        {
            return _uow.ProductRepository.GetAll().ToList();
        }

        public List<string> GetAllCategories()
        {
            return _uow.DbContext.Products.Select(p => p.Category).Distinct().ToList();
        }

        public async Task<Product> SaveProduct(Product product, Image image)
        {
            image.ProductId = product.Id;
            var existing = _uow.ImageRepository.Find(x => x.ProductId.Equals(product.Id));
            if (existing != null)
                existing = await _uow.ImageRepository.UpdateAsync(existing, image);
            else
                existing = await _uow.ImageRepository.AddAsync(image);
            product.Image = existing;
            return await _uow.ProductRepository.UpdateAsync(product, product.Id);
        }

        public Task<Product> AddProduct(Product product)
        {
            return _uow.ProductRepository.AddAsync(product);
        }

        public Product GetProduct(int id)
        {
            return _uow.ProductRepository.Get(id);
        }

        public List<Product> SearchProducts(List<string> catList, int? minPrice, int? maxPrice, string searchString)
        {
            var predicate = PredicateBuilder.True<Product>()
                .And(FilterCategories(catList))
                .And(FilterPrice(minPrice, maxPrice))
                .And(FilterSearchString(searchString));
            return new List<Product>(_uow.ProductRepository.FindAll(predicate));
        }

        public Task<Product> SaveProduct(Product product)
        {
            return _uow.ProductRepository.UpdateAsync(product, product.Id);
        }

        private static Expression<System.Func<Product, bool>> FilterCategories(ICollection<string> catList)
        {
            if (catList == null) return x => true;
            return x => catList.Contains(x.Category);
        }

        private static Expression<System.Func<Product, bool>> FilterPrice(int? minPrice, int? maxPrice)
        {

            if (minPrice == null || maxPrice == null) return x => true;
            return x => minPrice <= x.Price && x.Price <= maxPrice;
        }

        private static Expression<System.Func<Product, bool>> FilterSearchString(string searchString)
        {
            if (searchString == null || searchString.Trim() == string.Empty) return x => true;
            return x => x.Description.Contains(searchString) || x.Name.Contains(searchString);
        }
    }
}