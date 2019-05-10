using MedicalCare.Models.Dtos;
using MedicalCare.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Areas.Admin.Data.IServices
{
    interface ILaboratoristService
    {
        Task<Laboratorist> GetLaboratoristAsync(int? id);
        Task<Laboratorist> GetLaboratoristWithoutIdAsync();
        Task EditLaboratoristAsync(LaboratoristDto models, List<IFormFile> files);

        Task<List<Laboratorist>> ListLaboratoristAsync();
    }
}
