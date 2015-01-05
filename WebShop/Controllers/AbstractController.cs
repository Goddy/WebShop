using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebShop.Models;
using WebShop.Services;

namespace WebShop.Controllers
{
    public abstract class AbstractController : Controller
    {
        private readonly IAccountService _accountService;

        protected AbstractController(IAccountService service)
        {
            _accountService = service;
        }

        protected ApplicationUser GetUser()
        {
            return _accountService.GetAccount(User.Identity.GetUserId());
        }

        protected bool IsAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }

        protected void AddStatusMessage(string statusMessage)
        {
            TempData["StatusMessage"] = statusMessage;
        }
    }
}