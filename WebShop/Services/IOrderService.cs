using System.Collections.Generic;
using WebShop.Models;

namespace WebShop.Services
{
    /// <summary>
    /// Summary description for IOrderService
    /// </summary>
    public interface IOrderService
    {
        List<Order> GetAllOrders();
    }
}