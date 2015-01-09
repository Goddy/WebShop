using System.Collections.Generic;
using System.Linq;

namespace WebShop.Models
{
    /// <summary>
    /// Summary description for Order
    /// </summary>
    public class Order : AbstractEntity<int>
    {
        public Order()
        {
            OrderProducts = new List<OrderProduct>();
        }

        public Status Status { get; set; }
    
        public virtual ApplicationUser Account { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        public double GetTotalPrice()
        {
            return OrderProducts.Sum(product => product.Total);
        }
    }
}