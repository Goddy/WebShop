using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebShop.Models;

namespace WebShop.Contexts
{
    /// <summary>
    /// WebShopContext: Db context for webshop project
    /// </summary>
    public class WebShopContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}