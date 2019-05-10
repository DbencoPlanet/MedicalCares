using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Models.Entities
{
    public class Report
    {
        public int Id { get; set; }
        
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public DateTime Date { get; set; }

        public ReportType ReportType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public string Picture { get; set; }

        public EntityStatus Status { get; set; }

  
    }
}
