using MedicalCare.Models.Dtos;
using MedicalCare.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Areas.Admin.Data.IServices
{
    interface IReceptionistService
    {
        Task<Receptionist> GetReceptionistAsync(int? id);
        Task<Receptionist> GetReceptionistWithoutIdAsync();
        Task EditReceptionistAsync(ReceptionistDto models, List<IFormFile> files);

        Task<List<Receptionist>> ListReceptionistAsync();
    }
}
