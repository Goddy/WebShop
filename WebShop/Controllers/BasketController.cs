using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Services;

namespace WebShop.Controllers
{
    public class BasketController : Controller
    {
        private IOrderService _orderService;

        public BasketController() { }

        public BasketController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [AllowAnonymous]
        public ActionResult CheckOut()
        {
            var cartProducts = (Session["cartProducts"] as HashSet<int>) ?? new HashSet<int>();
            return View(_orderService.BuildOrderFromBasket(cartProducts));
        }
    }
}