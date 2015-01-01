using System.Collections.Generic;
using System.Linq;
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
            return _uow.OrderRepository.GetAll().ToList<Order>();
        }
    }
}