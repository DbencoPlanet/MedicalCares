using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Models.Entities
{
    public class Account
    {
        public int Id { get; set; }

        [Display(Name = "Account Name")]
        public string Name { get; set; }

        [Display(Name="Account Type")]
        public AccountType Type { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public EntityStatus Status { get; set; }

    }
}
