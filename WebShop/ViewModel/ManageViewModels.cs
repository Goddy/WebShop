using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using WebShop.App_GlobalResources;
using WebShop.Models;

namespace WebShop.ViewModel
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        public ApplicationUser User { get; set; }
        public Role Roles { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(Locale), ErrorMessageResourceName = "ValidationLengthText", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Locale), Name = "New_password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Locale), Name = "Confirm_new_password")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof (Locale), ErrorMessageResourceName = "Password_match_issue")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Locale), Name = "Current_password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(Locale), ErrorMessageResourceName = "ValidationLengthText", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Locale), Name = "New_password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Locale), Name = "Confirm_new_password")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof (Locale), ErrorMessageResourceName = "Password_match_issue")]
        public string ConfirmPassword { get; set; }
    }
}