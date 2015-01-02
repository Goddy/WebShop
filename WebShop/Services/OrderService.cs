using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;
using WebShop.Models;
using WebShop.Repositories;

namespace WebShop.Services
{
    /// <summary>
    /// Summary description for OrderService
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly UnitOfWork _uow;

        public OrderService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public List<Order> GetAllOrders()
        {
            return _uow.OrderRepository.GetAll().ToList();
        }

        public Order BuildAndSaveOrderFromCheckout(OrderProductList orderProductList, String userId)
        {
            //Make sure the user is fetched
            ApplicationUser user = _uow.Context.Users.Find(userId);
            //Create the order
            var order = new Order {Account = user};
            orderProductList.OrderProductModels.ForEach(x => order.OrderProducts.Add(new OrderProduct(_uow.ProductRepository.Get(x.ProductId), x.Amount)));
            _uow.OrderRepository.Add(order);
            return order;
        }

   public OrderProductList BuildOrderProductListFromBasket(HashSet<int> products)
        {
            var orderList = new OrderProductList();
            products.ForEach(x => orderList.OrderProductModels.Add(new OrderProductModel(_uow.ProductRepository.Get(x), 1)));
            return orderList;
        }
    }
}