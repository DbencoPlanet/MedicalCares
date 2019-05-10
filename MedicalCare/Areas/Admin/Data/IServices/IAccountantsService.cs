using MedicalCare.Models.Dtos;
using MedicalCare.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Areas.Admin.Data.IServices
{
    public interface IAccountantsService
    {
        Task <Accountants> GetAccountantAsync(int? id);
        Task<Accountants> GetAccountantWithoutIdAsync();
        Task EditAccountantAsync(AccountantDto models, List<IFormFile> files);

        Task<List<Accountants>> ListAccountantAsync();
    }
}
