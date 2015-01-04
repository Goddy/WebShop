using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using WebShop.Models;

namespace WebShop.Services
{
    /// <summary>
    /// Summary description for IProductService
    /// </summary>
    public interface IProductService
    {
        List<Product> GetAllProducts();
        List<String> GetAllCategories();
        Task<Product> SaveProduct(Product product);
        Task<Product> SaveProduct(Product product, Image image);
        Task<Product> AddProduct(Product product);
        Product GetProduct(int id);
        List<Product> SearchProducts(List<string> catList, int? minPrice, int? maxPrice, string searchString);
    }
}