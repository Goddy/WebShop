using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebShop.App_GlobalResources;
using WebShop.Models;

namespace WebShop.ViewModel
{

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

        [Display(ResourceType = typeof (Locale), Name = "Role")]
        public Role Role { get; set; }
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

    public class AccountsViewModel
    {
        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
        public IEnumerable<ApplicationRole> ApplicationRoles { get; set; }
        
        public AccountsViewModel(){}

        public AccountsViewModel(IEnumerable<ApplicationUser> users, IEnumerable<ApplicationRole> roles)
        {
            ApplicationUsers = users;
            ApplicationRoles = roles;
        }
    }
}
