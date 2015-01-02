using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.Models;
using WebShop.Repositories;

namespace WebShop.Services
{
    public class AccountService : IAccountService
    {
        private readonly UnitOfWork _uow;

        public AccountService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public ApplicationUser GetAccount(String id)
        {
            return _uow.Context.Users.Find(id);
        }
    }

    
}