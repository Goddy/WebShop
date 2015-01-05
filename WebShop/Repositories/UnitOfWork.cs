using System;
using WebShop.Contexts;
using WebShop.Models;

namespace WebShop.Repositories
{
    /// <summary>
    /// Summary description for UnitOfWork
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        private readonly WebShopContext _context;
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<Product> _productRepository;

        public UnitOfWork(WebShopContext webShopContext)
        {
            _context = webShopContext;
        }

        public GenericRepository<Order> OrderRepository
        {
            get { return _orderRepository ?? (_orderRepository = new GenericRepository<Order>(_context)); }
        }
        public GenericRepository<Product> ProductRepository
        {
            get { return _productRepository ?? (_productRepository = new GenericRepository<Product>(_context)); }
        }

        public WebShopContext Context
        {
            get { return _context; }
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}