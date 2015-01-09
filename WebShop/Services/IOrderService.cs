using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.Models;
using WebShop.ViewModel;

namespace WebShop.Services
{
    /// <summary>
    /// Summary description for IOrderService
    /// </summary>
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order BuildAndSaveOrder(OrderProductList orderProductList, ApplicationUser user);
        OrderProductList BuildOrderProductListFromBasket(HashSet<int> products);
        OrderProductList RepopulateProductOrderList(OrderProductList list);
        Task<bool> Remove(int id);
        Task<Order> Get(int id);
    }
}