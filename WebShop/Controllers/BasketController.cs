﻿using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebShop.Models;
using WebShop.Services;

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
            return View(_orderService.BuildOrderProductListFromBasket(cartProducts));
        }

        [AllowAnonymous]
        public void ReadyToPay()
        {
            //Set session attribute to go to overview after registration/login
            Session["readyToPay"] = true;
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderProductList orderProductList)
        {
            _orderService.BuildAndSaveOrderFromCheckout(orderProductList, User.Identity.GetUserId());
            TempData["StatusMessage"] = "Order Added";
            //Remove sessiondata
            Session["readyToPay"] = false;
            return Redirect("/Orders/MyOrders");
        }
        
    }
}