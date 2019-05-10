using MedicalCare.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Models.Dtos
{
    public class PatientDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Display(Name = "Patient ID")]
        public string PatientReg { get; set; }

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

        [Display(Name = " Email Address")]
        [Required]
        public string EmailAddress { get; set; }

        public string UserName { get; set; }

        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }

        [Display(Name = "MobileNo")]
        [Required]
        public string MobileNo { get; set; }

        public int BloodGroupId { get; set; }
        public BloodGroup BloodGroup { get; set; }

        [Display(Name = "Sex")]
        [UIHint("Enum")]
        public Sex Sex { get; set; }

        [Display(Name = "Date of Birth")]
        public string DOB { get; set; }

        [Display(Name = "Picture")]
        public string ProfilePicture { get; set; } = "/images/empty-profile.png";

        [Display(Name = "Address")]
        [Required]
        public string Address { get; set; }

        [Display(Name = "Status")]
        public EntityStatus Status { get; set; }
    }
}
