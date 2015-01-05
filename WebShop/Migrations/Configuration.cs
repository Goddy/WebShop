using System.Collections.Generic;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebShop.Models;

namespace WebShop.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Contexts.WebShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WebShop.Contexts.WebShopContext";
        }

        protected override void Seed(Contexts.WebShopContext context)
        {
            var applicationUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context), context);
            var users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    UserName = "jos@jos.be",
                    Email = "jos@jos.be",
                    Name = "Jos",
                    Address =
                        new Address
                        {
                            Street = "Josstraat",
                            Number = 24,
                            City = "Josstad",
                            PostalCode = 3290,
                            Country = "Belgie"
                        }
                },
                new ApplicationUser
                {
                    UserName = "tom@tom.be",
                    Email = "tom@tom.be",
                    Name = "Tom",
                    Address =
                        new Address
                        {
                            Street = "Tomstraat",
                            Number = 24,
                            City = "Tomstad",
                            PostalCode = 3290,
                            Country = "Belgie"
                        }
                },
                new ApplicationUser
                {
                    UserName = "jan@jan.be",
                    Email = "jan@jan.be",
                    Name = "Jan",
                    Address =
                        new Address
                        {
                            Street = "Janstraat",
                            Number = 24,
                            City = "Janstad",
                            PostalCode = 3290,
                            Country = "Belgie"
                        }
                },
                new ApplicationUser
                {
                UserName = "admin@admin.be",
                Email = "admin@admin.be",
                Name = "Admin",
                Address =
                    new Address
                    {
                        Street = "Adminstraat",
                        Number = 24,
                        City = "Adminstad",
                        PostalCode = 3290,
                        Country = "Belgie"
                    }
                }
            };
            if (!Roles.RoleExists("admin"))
            {
                Roles.CreateRole("admin");
                Roles.AddUserToRole("admin@admin.be", "admin");
            }
            foreach (var applicationUser in users)
            {
                applicationUserManager.Create(applicationUser, "Password1!");
            }
            context.SaveChanges();
        }
    }
}
