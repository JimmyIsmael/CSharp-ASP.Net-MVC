using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CigarStoreProject.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Company name is required.")]
        public string CompanyName { get; set; }

        [Display(Name = "Tax Id")]
        [Required(ErrorMessage = "Tax id is required.")]
        [Range(000000000, 999999999, ErrorMessage = "Tax ID must be a nine digits value.")]
        public string TaxId { get; set; }

        [Display(Name = "Tobacco License Number")]
        [Required(ErrorMessage = "License Number is required.")]
        [Range(000000000, 999999999, ErrorMessage = "License must be a nine digits value.")]
        public string TobaccoLicense { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email address is required.")]
        public string EmailAddress { get; set; }

        [Display(Name = "Confirm Address")]
        [Compare("EmailAddress", ErrorMessage = "Email must match.")]
        public string ConfirmEmail { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Password must be within 10 and 20 characters")]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password must match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required.")]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "Zip Code is required.")]
        public string Zip { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = false;

        public bool IsAdmin { get; set; } = false;
    }
}