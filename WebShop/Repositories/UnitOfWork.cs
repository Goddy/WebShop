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
        private WebShopContext _context;
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<ApplicationUser> _userRepository;

        public GenericRepository<Order> OrderRepository
        {
            get { return _orderRepository ?? (_orderRepository = new GenericRepository<Order>(DbContext)); }
        }
        public GenericRepository<Product> ProductRepository
        {
            get { return _productRepository ?? (_productRepository = new GenericRepository<Product>(DbContext)); }
        }

        public GenericRepository<ApplicationUser> UserRepository
        {
            get { return _userRepository ?? (_userRepository = new GenericRepository<ApplicationUser>(DbContext)); }
        }

        public WebShopContext DbContext
        {
            get { return _context ?? (_context = new WebShopContext()); }
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}