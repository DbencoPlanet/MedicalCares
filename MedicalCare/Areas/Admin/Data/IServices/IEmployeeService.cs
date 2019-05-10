using MedicalCare.Models.Dtos;
using MedicalCare.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Areas.Admin.Data.IServices
{
    interface IEmployeeService
    {
        Task<Employee> GetEmployeeAsync(int? id);
        Task<Employee> GetEmployeeWithoutIdAsync();
        Task EditEmployeeAsync(EmployeeDto models, List<IFormFile> files);

        Task<List<Employee>> ListEmployeeAsync();
    }
}
