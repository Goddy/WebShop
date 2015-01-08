using System;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using WebShop.Services;

namespace WebShop.Controllers
{
    public class OrdersController : AbstractController
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IOrderService _orderService;

        public OrdersController(ApplicationUserManager accountService, IAuthenticationManager authenticationManager, IOrderService orderService)
            : base(accountService)
        {
            _authenticationManager = authenticationManager;
            _orderService = orderService;
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

        [Authorize(Roles = "Admin, Assistent")]
        public ActionResult Overview()
        {
            return View(_orderService.GetAllOrders());
        }
    }
}