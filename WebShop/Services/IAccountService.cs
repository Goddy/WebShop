using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.Services
{
    public interface IAccountService
    {
        ApplicationUser GetAccount(String id);
    }
}
