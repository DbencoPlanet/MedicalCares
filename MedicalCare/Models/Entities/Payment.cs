using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Models.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public DateTimeOffset Date { get; set; }

        public int AcctId { get; set; }
        public Account Account { get; set; }

        [Display(Name="Pay To")]
        public string PayTo { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Amount")]
        public double Ammount { get; set; }

        [Display(Name = "Status")]
        public EntityStatus Status { get; set; }
    }
}
