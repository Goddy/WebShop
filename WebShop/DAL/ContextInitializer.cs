using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Security;
using WebShop.Contexts;
using WebShop.Models;

namespace WebShop.DAL
{
    public class ContextInitializer : DropCreateDatabaseAlways<WebShopContext>
    {
        private readonly ApplicationUserManager _applicationUserManager;

        public ContextInitializer(ApplicationUserManager applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
        }
        protected override async void Seed(WebShopContext context)
        {
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
            Roles.AddUserToRole("admin@admin.be", "admin");
            foreach (var applicationUser in users)
            {
                await _applicationUserManager.CreateAsync(applicationUser, "Password1!");
                context.Users.Add(applicationUser);
            }
            context.SaveChanges();
        }
    }
}