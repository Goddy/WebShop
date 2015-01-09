using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using WebShop.ViewModel;

namespace WebShop.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly ApplicationSignInManager _signInManager;
        private readonly ApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly ApplicationRoleManager _applicationRoleManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IAuthenticationManager authenticationManager, ApplicationRoleManager applicationRoleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
            _applicationRoleManager = applicationRoleManager;
        }

        

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                _authenticationManager.SignOut();
                return RedirectToAction("Index", "Products");
            }
            
            var model = new IndexViewModel
            {
                HasPassword = user.PasswordHash != null,
                PhoneNumber = user.PhoneNumber,
                TwoFactor = user.TwoFactorEnabled,
                Logins = await _userManager.GetLoginsAsync(user.Id),
                BrowserRemembered = await _authenticationManager.TwoFactorBrowserRememberedAsync(user.Id),
                User = user,
                Roles = GetRoleForUser(user)
            };
            return View(model);
        }

        [ActionName("EditAction")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index(string id, ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "The password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "The password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "The two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "The phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "The phone number was removed."
                : "";

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return View("Error");
            }
            var model = new IndexViewModel
            {
                HasPassword = user.PasswordHash != null,
                PhoneNumber = user.PhoneNumber,
                TwoFactor = user.TwoFactorEnabled,
                Logins = await _userManager.GetLoginsAsync(user.Id),
                BrowserRemembered = await _authenticationManager.TwoFactorBrowserRememberedAsync(user.Id),
                User = user,
                Roles = GetRoleForUser(user)
            };
            return View("Index", model);
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ViewResult> Save(IndexViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.User.Id);
            user.Name = model.User.Name;
            user.Email = model.User.Email;
            user.PhoneNumber = model.User.PhoneNumber;
            user.Address.Street = model.User.Address.Street;
            user.Address.Number = model.User.Address.Number;
            user.Address.City = model.User.Address.City;
            user.Address.PostalCode = model.User.Address.PostalCode;
            user.Address.Country = model.User.Address.Country;
            var userRole = GetRoleForUser(user);
            if (!model.Roles.Equals(userRole))
            {
                await _userManager.RemoveFromRoleAsync(user.Id, userRole.ToString());
                await _userManager.AddToRoleAsync(user.Id, model.Roles.ToString());
            }
            await _userManager.UpdateAsync(user);
            return View("Index", model);
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private Role GetRoleForUser(IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim> user)
        {
            var firstOrDefault = _applicationRoleManager.Roles.ToList().FirstOrDefault(x =>
            {
                var identityUserRole = user.Roles.FirstOrDefault();
                return identityUserRole != null && x.Id.Equals(identityUserRole.RoleId);
            });
            if (firstOrDefault == null)
                return Role.User;
            var roleName = firstOrDefault.Name;
            Role role;
            if (!Enum.TryParse(roleName, false, out role))
                role = Role.User;
            return role;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}