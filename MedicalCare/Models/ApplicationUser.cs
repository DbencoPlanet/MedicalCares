using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MedicalCare.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace MedicalCare.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Surname")]
        [Required]
        public string SurName { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Other Name")]
        public string OtherName { get; set; }

        public string Fullname
        {
            get
            {
                return SurName + " " + FirstName + " " + OtherName;
            }
        }


        [Display(Name = "MobileNo")]
        [Required]
        public string MobileNo { get; set; }

        [Display(Name = "Sex")]
        [UIHint("Enum")]
        public Sex Sex { get; set; }

        [Display(Name = "Picture")]
        public string ProfilePicture { get; set; } = "/upload/blank-person.png";

        [Display(Name = "Address")]
        [Required]
        public string Address { get; set; }

        [Display(Name = "Status")]
        public EntityStatus EntityStatus { get; set; }

        [Display(Name = "Doctor ID")]
        public string DoctorReg { get; set; }

        [Display(Name = "Employee ID")]
        public string EmployeeReg { get; set; }

        [Display(Name = "Patient ID")]
        public string PatientReg { get; set; }


        public string RegisteredBy { get; set; }
    }
}
