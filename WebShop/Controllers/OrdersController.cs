using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Controllers
{
    public class OrdersController : AbstractController
    {
        public OrdersController(IAccountService accountService) : base (accountService)
        {

        }
        // GET: Order
        [Authorize]
        public ActionResult MyOrders()
        {
            return View(GetUser().Orders);
        }
    }
}