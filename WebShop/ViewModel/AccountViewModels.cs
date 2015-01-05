using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebShop.App_GlobalResources;

namespace WebShop.ViewModel
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(ResourceType = typeof (Locale), Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(ResourceType = typeof (Locale), Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(ResourceType = typeof (Locale), Name = "Remember_browser")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(ResourceType = typeof (Locale), Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(ResourceType = typeof (Locale), Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Locale), Name = "Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof (Locale), Name = "Remember_me")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(ResourceType = typeof (Locale), Name = "Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(ResourceType = typeof (Locale), Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(Locale), ErrorMessageResourceName = "ValidationLengthText", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Locale), Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Locale), Name = "Confirm_password")]
        [Compare("Password", ErrorMessageResourceType = typeof (Locale), ErrorMessageResourceName = "Password_Match")]
        public string ConfirmPassword { get; set; }

        [Display(ResourceType = typeof (Locale), Name = "Street")]
        [Required]
        public string Street { get; set; }

        [Display(ResourceType = typeof (Locale), Name = "Number")]
        [Required]
        public int Number { get; set; }

        [Display(ResourceType = typeof (Locale), Name = "Postal_Code")]
        [Required]
        public int PostalCode { get; set; }

        [Display(ResourceType = typeof (Locale), Name = "City")]
        [Required]
        public string City { get; set; }

        [Display(ResourceType = typeof (Locale), Name = "Country")]
        [Required]
        public string Country { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(ResourceType = typeof (Locale), Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof (Locale), ErrorMessageResourceName = "ValidationLengthText", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Locale), Name = "LoginViewModel_Password_Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Locale), Name = "Confirm_password")]
        [Compare("Password", ErrorMessageResourceType = typeof (Locale), ErrorMessageResourceName = "Password_Match")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(ResourceType = typeof (Locale), Name = "Email")]
        public string Email { get; set; }
    }
}
