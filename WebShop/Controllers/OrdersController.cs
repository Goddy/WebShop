using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using WebShop.Models;
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

        [Authorize]
        public async Task<ViewResult> Details(int? id)
        {
            if (id == null)
                return View("Error");
            var order = await _orderService.Get((int)id);
            return order == null ? View("Error") : View(order);
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
            var order = await _orderService.Get((int)id);
            return order == null ? View("Error") : View(order);
        }

        [Authorize(Roles = "Admin, Assistent")]
        public async Task<ViewResult> Remove(int? id)
        {
            if (id == null)
                return View("Error");
            await _orderService.Remove((int)id);
            return View("Overview", _orderService.GetAllOrders());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(Order model)
        {
            var order = await _orderService.Get(model.Id);
            order.Status = model.Status;
            return await _orderService.Save(order) ? View("Edit", order) : View("Error");
        }
    }
}