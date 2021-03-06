﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGrease.Css.Extensions;
using WebShop.Models;
using WebShop.Repositories;
using WebShop.ViewModel;

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

        public Order BuildAndSaveOrder(OrderProductList orderProductList, ApplicationUser user)
        {
            //Make sure the user is fetched
            var newUser = _uow.UserRepository.Get(user.Id);
            //Create the order
            var order = new Order { Account = newUser, Status = Status.Paid};

            orderProductList.OrderProductModels.ForEach(x => order.OrderProducts.Add(new OrderProduct { Amount = x.Amount, Product = _uow.ProductRepository.Get(x.ProductId) }));
            _uow.OrderRepository.Add(order);
            return order;
        }

        public OrderProductList BuildOrderProductListFromBasket(List<int> products)
        {
            var orderList = new OrderProductList();
            products.Distinct().ForEach(x => orderList.OrderProductModels.Add(new OrderProductModel(_uow.ProductRepository.Get(x), products.Count(y=>y.Equals(x)))));
            return orderList;
        }


        public OrderProductList RepopulateProductOrderList(OrderProductList list)
        {
            list.OrderProductModels.ForEach(x => x.Product = _uow.ProductRepository.Get(x.ProductId));
            return list;
        }

        public async Task<bool> Remove(int id)
        {
            try
            {
                var order = await _uow.OrderRepository.GetAsync(id);
                if (order == null)
                    return false;
                await _uow.OrderRepository.DeleteAsync(order);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<Order> Get(int id)
        {
            return await _uow.OrderRepository.GetAsync(id);
        }

        public async Task<bool> Save(Order order)
        {
            try
            {
                await _uow.OrderRepository.UpdateAsync(order, order.Id);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}