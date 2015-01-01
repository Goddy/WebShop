using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private UnitOfWork _uow;

        public OrderService()
        {
            _uow = new UnitOfWork();
        }

        public List<Order> GetAllOrders()
        {
            return _uow.OrderRepository.GetAll().ToList();
        }

        public Order BuildOrderFromBasket(HashSet<int> products)
        {
            var order = new Order();
            Parallel.ForEach(products, x => order.OrderProducts.Add(new OrderProduct(_uow.ProductRepository.Get(x), 1)));
            return order;
        }
    }
}