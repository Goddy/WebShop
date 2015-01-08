using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebShop.Services;
using WebShop.ViewModel;

namespace WebShop.Controllers
{
    public class BasketController : AbstractController
    {
        private readonly IOrderService _orderService;

        public BasketController(IOrderService orderService, ApplicationUserManager applicationUserManager) : base (applicationUserManager)
        {
            _orderService = orderService;
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
            _orderService.BuildAndSaveOrder(orderProductList, GetUser());
            AddStatusMessage("Order successfully processed.");
            Session["order"] = null;
            return RedirectToAction("MyOrders","Orders");
        }

        [Authorize]
        public ActionResult Remove(int? id)
        {
            var order = (OrderProductList)Session["order"];
            order.OrderProductModels.RemoveAll(x => x.ProductId.Equals(id));
            Session["order"] = order;
            return View("CheckOut", order);
        }
    }
}