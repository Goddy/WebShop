namespace WebShop.Models
{
    /// <summary>
    /// OrderProduct with a many to many rel to prod
    /// </summary>
    public class OrderProduct : AbstractEntity<int>
    {
        public virtual Product Product { get; set; }
        public int Amount { get; set; }
        public virtual Order order { get; set; }

        public double getTotalPrice() { 
            return Product.Price * Amount;
        }
    }
}