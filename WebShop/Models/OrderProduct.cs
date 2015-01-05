namespace WebShop.Models
{
    /// <summary>
    /// OrderProduct with a many to many rel to prod
    /// </summary>
    public sealed class OrderProduct : AbstractEntity<int>
    {
        public OrderProduct()
        {
        }

        public OrderProduct(Product product, int amount)
        {
            Product = product;
            Amount = amount;
        }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public Order Order { get; set; }

        public double Total
        {
            get { return Product != null ? Amount*Product.Price : 0; }
        }
    }
}