using System;
using System.Collections.Generic;
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
    }
}