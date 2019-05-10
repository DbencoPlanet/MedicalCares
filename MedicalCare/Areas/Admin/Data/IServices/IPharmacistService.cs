using MedicalCare.Models.Dtos;
using MedicalCare.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Areas.Admin.Data.IServices
{
    interface IPharmacistService
    {
        Task<Pharmacist> GetPharmacistAsync(int? id);
        Task<Pharmacist> GetPharmacistWithoutIdAsync();
        Task EditPharmacistAsync(PharmacistDto models, List<IFormFile> files);

        Task<List<Pharmacist>> ListPharmacistAsync();
    }
}
