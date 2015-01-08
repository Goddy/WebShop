using System;
using System.Web.Mvc;
using Microsoft.Owin.Security;

namespace WebShop.Controllers
{
    public class OrdersController : AbstractController
    {
        private readonly IAuthenticationManager _authenticationManager;

        public OrdersController(ApplicationUserManager accountService, IAuthenticationManager authenticationManager)
            : base(accountService)
        {
            _authenticationManager = authenticationManager;
        }
        // GET: Order
        [Authorize]
        public ActionResult MyOrders()
        {
            try
            {
                return View(GetUser().Orders);
            }
            catch (Exception)
            {
                _authenticationManager.SignOut();
                return RedirectToAction("Index", "Products");
            }
        }
    }
}