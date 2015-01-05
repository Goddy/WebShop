using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebShop.Models;
using WebShop.Services;
using WebShop.ViewModel;

namespace WebShop.Controllers
{
    public class BasketController : AbstractController
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IOrderService _orderService;

        public BasketController(IOrderService orderService, ApplicationUserManager applicationUserManager, IAccountService accountService) : base (accountService)
        {
            _orderService = orderService;
            _userManager = applicationUserManager;
        }

        [AllowAnonymous]
        public ActionResult CheckOut()
        {
            var cartProducts = (Session["cartProducts"] as HashSet<int>) ?? new HashSet<int>();
            var order = _orderService.BuildOrderProductListFromBasket(cartProducts);
            Session["order"] = order;
            return View(order);
        }

        [AllowAnonymous]
        public void ReadyToPay()
        {
            //Set session attribute to go to overview after registration/login
            Session["readyToPay"] = true;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderProductList orderProductList)
        {
            Session["order"] = orderProductList;
            Session["readyToPay"] = true;
            return RedirectToAction("Payment");
            
        }

        [Authorize]
        [HttpGet]
        public ActionResult Payment()
        {
            Session["readyToPay"] = false;
            var orderProductList = Session["order"] as OrderProductList;
            ViewBag.address = GetUser().Address;
            //Todo: Since the product object disappears in the model, we need to repopulate those objects, why????
            return View(_orderService.RepopulateProductOrderList(orderProductList));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Payment(OrderProductList list)
        {
            var orderProductList = Session["order"] as OrderProductList;
            _orderService.BuildAndSaveOrder(orderProductList, User.Identity.GetUserId());
            AddStatusMessage("Order successfully processed.");
            Session["order"] = null;
            return RedirectToAction("MyOrders","Orders");
        }
    }
}