using System.Data.Entity;
using WebShop.Contexts;

namespace WebShop.DAL
{
    public class ForceDeleteInitializer : IDatabaseInitializer<WebShopContext>
    {
        private readonly IDatabaseInitializer<WebShopContext> _initializer;

        public ForceDeleteInitializer(IDatabaseInitializer<WebShopContext> innerInitializer)
        {
            _initializer = innerInitializer;
        }

        public void InitializeDatabase(WebShopContext context)
        {
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "ALTER DATABASE Webshop SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            _initializer.InitializeDatabase(context);
        }
    }
}