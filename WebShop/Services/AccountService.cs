using System;
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
            return _uow.UserRepository.Get(id);
        }
    }

    
}