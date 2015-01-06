using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            context.Database.Delete();
            context.Database.Create();
            var applicationUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context), context);
            Debug.WriteLine("Adding users");
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
            Debug.WriteLine("Adding products");
            var products = new List<Product>
            {
                new Product{Category = "Rollerblade", Description = "Eric's next pro skates comes in a strictly limited edition. Worldwide there are only 300 pairs available and every pair has its production number engraved.", Name = "VALO EB.1.5 LE Create Edition", Price = 319.95},
                new Product{Category = "Rollerblade", Description = "After the huge success of the first and second USD Carbon Morales, Franky is getting a new pro skate - his third USD Carbon Pro model. For first time the shell is made out of basalt fiber, which is same strong, durable and lightweight as the rest of the USD Carbon fiber shells but more environmental friendly and with a beautiful and unique black matt finish. The bottom and lower sidewalls are still reinforced with carbon and kevlar layers.", Name = "USD Carbon Franky Morales FLT III Edition", Price = 379.95},
                new Product{Category = "Rollerblade", Description = "USD is proud to present this extremely high quality Carbon-Free skate for Richie Eisler. Richie paid exceptional attention to detail when creating this skate. The materials are of the highest quality genuine leather and match his favourite shoes. The beauty of these skates is that the more worn they become, the better they look!", Name = "USD Carbon-Free Richie Eisler 3 Grey BootOnly", Price = 279.95},
                new Product{Category = "Frame", Description = "The next generation of Ground Control frames have arrived, introducing The BIGs.  A collaborative effort with team riders and our design staff, the BIGs are built to offer GC’s trusted street attributes blended with smooth roll of a recreational frame setup.  By increasing the height of the frame, increasing the width of the h-block, and lowering the side walls and outer edges of the center groove, we are now able to offer a frame that allows a maximum wheel size of 72mm that skates like a street frame.  Complete with aluminum spacers, your wheels will roll smooth and fast with minimal resistance.", Name = "GROUNDCONTROL BIG Frame Khaki", Price = 59.99},
                new Product{Category = "Frame", Description = "The Kizer Fluid is Kizer's longest running frame. Its continuous success is credited to its simple design, solid yet lightweight construction and its speed and consistency on the grind.", Name = "KIZER Fluid 4 Franky Clear Frame", Price = 69.99},
            };
            var images = new List<Image>
            {
                new Image("~/Images/Custom/bailey.jpg", null),
                new Image("~/Images/Custom/eisler.jpg", null),
                new Image("~/Images/Custom/franky.jpg", null),
                new Image("~/Images/Custom/franky_clear.jpg", null),
                new Image("~/Images/Custom/gc_white.jpg", null)
            };
            products = context.Products.AddRange(products).ToList();
            for (var i = 0; i < images.Count; i++)
            {
                images[i].ProductId = products[i].Id;
                products[i].Image = images[i];
            }
            context.Images.AddRange(images);
            context.SaveChanges();
        }
    }
}
