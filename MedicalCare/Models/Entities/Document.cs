using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Models.Entities
{
    public class Document
    {
        public int Id { get; set; }

        [Display(Name="Pateint ID")]
        [Required]
        public int? PateintId { get; set; }
        public Patient Patient { get; set; }

        [Display(Name = "Attach File")]
        [Required]
        public string File { get; set; }

        [Required]
        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
