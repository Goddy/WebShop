using System;
using System.Collections.Generic;
using System.Linq;
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

        public Order BuildAndSaveOrder(OrderProductList orderProductList, String userId)
        {
            //Make sure the user is fetched
            var user = _uow.Context.Users.Find(userId);
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
    

       public OrderProductList RepopulateProductOrderList(OrderProductList list)
        {
            list.OrderProductModels.ForEach(x => x.Product = _uow.ProductRepository.Get(x.ProductId));
            return list;
        }
    }
}