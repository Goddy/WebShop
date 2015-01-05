using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using WebShop.App_GlobalResources;

namespace WebShop.ViewModel
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
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

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(ResourceType = typeof (Locale), Name = "Phone_Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(ResourceType = typeof (Locale), Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(ResourceType = typeof (Locale), Name = "Phone_Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}