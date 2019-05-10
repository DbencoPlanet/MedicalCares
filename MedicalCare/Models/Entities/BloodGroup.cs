using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Models.Entities
{
    public class BloodGroup
    {
        public int Id { get; set; }

        [Display(Name= "Blood Group")]
        public string Name { get; set; }
    }
}
