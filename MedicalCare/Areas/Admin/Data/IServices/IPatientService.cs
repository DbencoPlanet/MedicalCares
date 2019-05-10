using MedicalCare.Models.Dtos;
using MedicalCare.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Areas.Admin.Data.IServices
{
    public interface IPatientService
    {
        Task<Patient> GetPatientAsync(int? id);
        Task<Patient> GetPatientWithoutIdAsync();
        Task EditPatientAsync(PatientDto models, List<IFormFile> files);

        Task<List<Patient>> ListPatientAsync();
    }
}
