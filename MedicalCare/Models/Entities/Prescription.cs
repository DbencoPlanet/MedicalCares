using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Models.Entities
{
    public class Prescription 
    {
        public int Id { get; set; }

        public int? PatientId { get; set; }
        public Patient Patient { get; set; }

        public string Weight { get; set; }

        [Display(Name = "Blood Pressure")]
        public string BloodPressure { get; set; }

        [Display(Name = "Reference")]
        public string Reference { get; set; }

        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [Display(Name = "Case Study")]
        public string CaseStudy { get; set; }

        public List<PrescriptionLine> PrescriptionLine { get; set; } = new List<PrescriptionLine>();

        //public int? DiagnosisId { get; set; }
        //public Diagnosis Diagnosis { get; set; }

        public List<Diagnosis> Diagnosis { get; set; } = new List<Diagnosis>();


        [Display(Name = "Visiting Fees")]
        public string VisitingFees { get; set; }

        [Display(Name = "Patient Note")]
        public string PatientNote { get; set; }

        public DateTime Date { get; set; }




    }

}
