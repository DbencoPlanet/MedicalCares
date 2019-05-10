using MedicalCare.Models.Dtos;
using MedicalCare.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Areas.Admin.Data.IServices
{
    interface INurseService
    {
        Task<Nurse> GetNurseAsync(int? id);
        Task<Nurse> GetNurseWithoutIdAsync();
        Task EditNurseAsync(NurseDto models, List<IFormFile> files);

        Task<List<Nurse>> ListNurseAsync();
    }
}
