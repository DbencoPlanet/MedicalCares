using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Models.Entities
{
    public class PrescriptionLine
    {
        public int Id { get; set; }

        //[Display(Name= "Medicine Name")]
        //public string MedicineName { get; set; }

        public int? MedicineId { get; set; }
        [Display(Name = "Medicine Name")]
        public Medicine Medicine { get; set; }

        //[Display(Name = "Medicine Type")]
        //public string MedicineType { get; set; }

        public int? MedCatId { get; set; }
        [Display(Name = "Medicine Type")]
        public MedicineCategory MedicineCategory { get; set; }

        [Display(Name = "Instruction")]
        public string Instruction { get; set; }

        [Display(Name = "Days")]
        public string Days { get; set; }

        public int? PresId { get; set; }
        public Prescription Prescription { get; set; }

    }
}
