using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using WebShop.Contexts;
using WebShop.Controllers;
using WebShop.Models;
using WebShop.Repositories;
using WebShop.Services;

namespace WebShop
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IAccountService, AccountService>();

            //A bit of IoC txeaking with Identity (http://tech.trailmax.info/2014/09/aspnet-identity-and-ioc-container-registration/)
            container.RegisterType<ApplicationSignInManager>();
            container.RegisterType<WebShopContext>();
            container.RegisterType<UnitOfWork>();
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
            new InjectionConstructor(typeof(WebShopContext)));
        }
    }
}