using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Models.Entities
{
    public class BedAllotment
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int BedCatId { get; set; }
        public BedCategory BedCategory { get; set; }

        [Display(Name = "Allotment Time")]
        public DateTime AlotmentTime { get; set; }

        [Display(Name = "Discharge Time")]
        public DateTime DischargTime { get; set; }

        [Display(Name = "Day")]
        public int Day { get; set; }

        [Display(Name = "Total")]
        public int Total { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Status")]
        public BedStatus Status { get; set; }


    }
}
