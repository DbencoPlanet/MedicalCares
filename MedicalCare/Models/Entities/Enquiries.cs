using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Models.Entities
{
    public class Enquiries
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string Enquiry { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}
