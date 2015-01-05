using System;
using WebShop.Models;

namespace WebShop.Services
{
    public interface IAccountService
    {
        ApplicationUser GetAccount(String id);
    }
}
