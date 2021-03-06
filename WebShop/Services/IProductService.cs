﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        Task<Product> AddProduct(Product product, Image image);
        Task<Product> DeleteProduct(int id);
        Product GetProduct(int id);
        List<Product> SearchProducts(List<string> catList, int? minPrice, int? maxPrice, string searchString);
    }
}