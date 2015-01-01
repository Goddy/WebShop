using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using WebShop.Controllers;
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

            //Register controllers without DI
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());

        }
    }
}