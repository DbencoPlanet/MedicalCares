using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Models.Entities
{
    public class Medicine
    {
        public int Id { get; set; }

        [Display(Name="Medicine Name")]
        public string Name { get; set; }

        public int MedCatId { get; set; }
        public MedicineCategory MedicineCategory { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        //[Display(Name = "Days")]
        //public string Days { get; set; }

        public double Price { get; set; }

        [Display(Name = "Manufactured By")]
        public string ManufacturedBy { get; set; }

        public EntityStatus Status { get; set; }

        
    }
}
