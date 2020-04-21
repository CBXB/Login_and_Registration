using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login_and_Registration.Models
{
    public class User
    {
        [Key]
        public int name_id { get; set; }
        
        [Required]
        [MinLength(2)]
        [Display(Name="First Name:")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name="Last Name:")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name="Email:")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [Display(Name="Password:")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [NotMapped]
        [Compare("Password")]
        [MinLength(8)]
        [Display(Name="Confirm Password:")]
        public string ConfirmPassword { get; set; }

        [EmailAddress]
        [Display(Name="Email:")]
        [NotMapped]
        public string LoginEmail { get; set; }

        [DataType(DataType.Password)]
        [NotMapped]
        [Display(Name="Password")]
        public string LoginPassword { get; set; }
        
    }
}