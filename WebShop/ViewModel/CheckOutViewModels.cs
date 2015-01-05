using System.Collections.Generic;
using System.Linq;
using WebShop.Models;

namespace WebShop.ViewModel
{
    public class OrderProductModel
    {
        public OrderProductModel()
        {
        }

        public OrderProductModel(Product product, int amount)
        {
            Amount = amount;
            Product = product;
            ProductId = product.Id;
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
            OrderProductModels = new List<OrderProductModel>();
        }
        public List<OrderProductModel> OrderProductModels { get; set; }
        public double Total
        {
            get { return OrderProductModels.Sum(product => product.Total); }
        }
    }
}