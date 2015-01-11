using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebShop.Models;

namespace WebShop.Controllers
{
    public abstract class AbstractController : Controller
    {
        protected readonly ApplicationUserManager UserManager;

        protected AbstractController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        protected ApplicationUser GetUser()
        {
            var userid = User.Identity.GetUserId();
            return UserManager.Users.FirstOrDefault(x => x.Id.Equals(userid));
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