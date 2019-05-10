using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Models.Entities
{
    public class Diagnosis
    {
        public int Id { get; set; }

        [Display(Name="Diagnosis")]
        public string Name { get; set; }

        [Display(Name = "Instruction")]
        public string Instruction { get; set; }

        public int? PresId { get; set; }
        public Prescription Prescription { get; set; }
    }
}
