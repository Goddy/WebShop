using System;
using System.Data.Entity;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebShop.Contexts;

namespace WebShop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
            //Reinitialize DB
            //Database.SetInitializer<WebShopContext>(null);
        }

        private void Application_BeginRequest(Object source, EventArgs e)
        {
            var application = (HttpApplication)source;
            var context = application.Context;

            string culture = null;
            if (Request.UserLanguages == null ||
                (context.Request.UserLanguages == null || Request.UserLanguages.Length <= 0)) 
                return;
            culture = Request.UserLanguages[0];
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }
    }
}
