using MedicalCare.Models;
using MedicalCare.Models.AccountViewModels;
using MedicalCare.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MedicalCare.Areas.Admin.Data.IServices
{
    interface IUserManagerService
    {
        Task<string> NewAccountantAsync(RegisterViewModel model, List<IFormFile> files);
        Task<Accountants> GetAccountantAsync(int? id);

        Task<string> NewLaboratoristAsync(RegisterViewModel model, List<IFormFile> files);
        Task<Laboratorist> GetLaboratoristAsync(int? id);

        Task<string> NewNurseAsync(RegisterViewModel model, List<IFormFile> files);
        Task<Nurse> GetNurseAsync(int? id);

        Task<string> NewPharmacistAsync(RegisterViewModel model, List<IFormFile> files);
        Task<Pharmacist> GetPharmacistAsync(int? id);

        Task<string> NewReceptionistAsync(RegisterViewModel model, List<IFormFile> files);
        Task<Receptionist> GetReceptionistAsync(int? id);

        Task<string> NewDoctorAsync(RegisterViewModel model, List<IFormFile> files,int BloodGpId, string designation,int deptId,string biography, string specialist, string education);
        Task<Doctor> GetDoctorAsync(int? id);

        Task<string> NewPatientAsync(RegisterViewModel model, List<IFormFile> files, int BloodGpId);
        Task<Patient> GetPatientAsync(int? id);


        Task<bool> UpdateDoctorAsync(ApplicationUser mode);
        Task<bool> UpdateAccountantAsync(ApplicationUser model);
        Task<bool> UpdateLaboratoristAsync(ApplicationUser model);
        Task<bool> UpdateNurseAsync(ApplicationUser model);
        Task<bool> UpdatePharmacistAsync(ApplicationUser model);
        Task<bool> UpdateReceptionistAsync(ApplicationUser model);
        Task<bool> UpdatePatientAsync(ApplicationUser model);


        Task Delete(int? id);
        Task<ApplicationUser> GetUserByUserIdAsync(string id);
        Task<ApplicationUser> GetUserByUserEmailAsync(string email);


        
      

        //Task<List<Doctor>> ListDoctorAsync(string searchString, string currentFilter, int? page);
        //Task<List<Employee>> ListEmployeeAsync(string searchString, string currentFilter, int? page);
        //Task<List<Patient>> ListPatientAsync(string searchString, string currentFilter, int? page);
        Task<List<ApplicationUser>> AllUsersAsync(string searchString, string currentFilter, int? page);
        Task<List<ApplicationUser>> UsersAsync();

        Task<bool> IsUsersinRoleAsync(string userid, string role);
        Task<bool> UpdateUserAsync(ApplicationUser model);
        Task AddUserToRoleAsync(string userId, string rolename);
        Task RemoveUserToRoleAsync(string userId, string rolename);

        Task<List<ApplicationUser>> UserAllAsync();
    }
}
