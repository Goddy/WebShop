using System;
using System.Threading.Tasks;
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

        [Authorize(Roles = "Admin, Assistent")]
        public async Task<ViewResult> Edit(int? id)
        {
            if (id == null)
                return View("Error");
            var order = await _orderService.Get((int) id);
            if (order == null)
                return View("Error");
            return View(order);
        }

        [Authorize(Roles = "Admin, Assistent")]
        public async Task<ViewResult> Remove(int? id)
        {
            if (id == null)
                return View("Error");
            await _orderService.Remove((int)id);
            return View("Overview", _orderService.GetAllOrders());
        }
    }
}