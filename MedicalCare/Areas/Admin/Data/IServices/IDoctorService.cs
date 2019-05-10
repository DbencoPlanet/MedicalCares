using MedicalCare.Models.Dtos;
using MedicalCare.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Areas.Admin.Data.IServices
{
    interface IDoctorService
    {
        Task<Doctor> GetDoctorAsync(int? id);
        Task<Doctor> GetDoctorWithoutIdAsync();
        Task EditDoctorAsync(DoctorDto models, List<IFormFile> files);

        Task<List<Doctor>> ListDoctorAsync();
    }
}
