using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebShop.Models;

namespace WebShop.Controllers
{
    public abstract class AbstractController : Controller
    {
        private readonly ApplicationUserManager _userManager;

        protected AbstractController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        protected ApplicationUser GetUser()
        {
            var userid = User.Identity.GetUserId();
            return _userManager.Users.FirstOrDefault(x => x.Id.Equals(userid));
        }

        protected bool IsAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }

        protected void AddStatusMessage(string statusMessage)
        {
            TempData["StatusMessage"] = statusMessage;
        }

        protected void AddData(string name, string value)
        {
            TempData[name] = value;
        }
    }
}