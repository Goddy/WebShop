using System;
using System.Data.Entity;
using WebShop.Contexts;
using WebShop.Models;

namespace WebShop.Repositories
{
    /// <summary>
    /// Summary description for UnitOfWork
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        private WebShopContext _context = new WebShopContext();
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<Product> _productRepository;

        public GenericRepository<Order> OrderRepository
        {
            get
            {

                if (this._orderRepository == null)
                {
                    this._orderRepository = new GenericRepository<Order>(_context);
                }
                return _orderRepository;
            }
        }
        public GenericRepository<Product> ProductRepository
        {
            get
            {

                if (this._productRepository == null)
                {
                    this._productRepository = new GenericRepository<Product>(_context);
                }
                return _productRepository;
            }
        }

        public WebShopContext Context
        {
            get { return _context; }
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}