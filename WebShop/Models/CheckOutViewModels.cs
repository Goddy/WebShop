using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class OrderProductModel
    {
        public OrderProductModel()
        {
        }

        public OrderProductModel(Product product, int amount)
        {
            this.Amount = amount;
            this.Product = product;
            this.ProductId = product.Id;
        }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public int ProductId { get; set; }
    
        public double Total
        {
            get { return Product != null ? Amount * Product.Price : 0; }
        }
    }

    public class OrderProductList
    {
        public OrderProductList()
        {
            this.OrderProductModels = new List<OrderProductModel>();
        }
        public List<OrderProductModel> OrderProductModels { get; set; }
        public double Total
        {
            get { return this.OrderProductModels.Sum(product => product.Total); }
        }
    }
}