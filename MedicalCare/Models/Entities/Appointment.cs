using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Models.Entities
{
    public class Appointment
    {
        public int Id { get; set; }

        public int DeptId { get; set; }
        [Display(Name= "Department Name")]
        public Department Department { get; set; }

        public int DoctorId { get; set; }
        [Display(Name = "Doctor Name")]
        public Doctor Doctor { get; set; }

        [Display(Name = "Appointment Date")]
        public DateTimeOffset AppointmentDate { get; set; }

        [Display(Name = "Problem")]
        public string Problem { get; set; }

        [Display(Name = "Status")]
        public EntityStatus Status { get; set; }


    }
}
