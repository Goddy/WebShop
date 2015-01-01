using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.EnterpriseServices;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Antlr.Runtime.Misc;
using Microsoft.Ajax.Utilities;
using WebShop.Contexts;
using WebShop.Models;
using WebShop.Repositories;
using System.Linq.Expressions;
using System.Web;
using LinqKit;

namespace WebShop.Services
{
    /// <summary>
    /// Provides product services to controllers.
    /// </summary>
    public class ProductService : IProductService
    {
        private UnitOfWork _uow;

        public ProductService()
        {
            _uow = new UnitOfWork();
        }

        public List<Product> GetAllProducts()
        {
            return _uow.ProductRepository.GetAll().ToList<Product>();
        }

        public List<string> GetAllCategories()
        {
            return _uow.Context.Products.Select(p => p.Category).Distinct().ToList();
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