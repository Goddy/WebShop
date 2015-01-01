using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Models
{
    /// <summary>
    /// OrderProduct with a many to many rel to prod
    /// </summary>
    public class OrderProduct : AbstractEntity<int>
    {
        public OrderProduct(Product product, int amount)
        {
            Product = product;
            Amount = amount;
        }
        public virtual Product Product { get; set; }
        public int Amount { get; set; }
        public virtual Order Order { get; set; }

        public double Total { get { return Amount * Product.Price ; } }
    }
}