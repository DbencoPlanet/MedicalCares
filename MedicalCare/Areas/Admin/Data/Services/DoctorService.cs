using MedicalCare.Areas.Admin.Data.IServices;
using MedicalCare.Data;
using MedicalCare.Models;
using MedicalCare.Models.Dtos;
using MedicalCare.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicalCare.Services;
using Microsoft.AspNetCore.Hosting;

namespace MedicalCare.Areas.Admin.Data.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDotnetdesk _dotnetdesk;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;


        public DoctorService(UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager,
           ApplicationDbContext context,
           SignInManager<ApplicationUser> signInManager,
           IHttpContextAccessor contextAccessor, IDotnetdesk dotnetdesk,
            IHostingEnvironment env
          )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
            _dotnetdesk = dotnetdesk;
            _env = env;

        }

        public async Task EditDoctorAsync(DoctorDto models, List<IFormFile> files)
        {
            var fileName = await _dotnetdesk.UploadFile(files, _env);

            Doctor doctor = await _context.Doctor.FindAsync(models.Id);
            doctor.Biography = models.Biography;
            doctor.Department = models.Department;
            doctor.Designation = models.Designation;
            doctor.Education = models.Education;
            doctor.ProfilePicture = "/uploads/" + fileName;
            doctor.Specialist = models.Specialist;
            doctor.PhoneNo = models.PhoneNo;
            doctor.Address = models.Address;
            doctor.BloodGroupId = models.BloodGroupId;
            doctor.DeptId = models.DeptId;
            doctor.EmailAddress = models.EmailAddress;
            doctor.FirstName = models.FirstName;
            doctor.OtherName = models.OtherName;
            doctor.SurName = doctor.SurName;
            doctor.Sex = models.Sex;
            doctor.Status = models.Status;
            doctor.PhoneNo = models.PhoneNo;
            
            _context.Entry(doctor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //   ApplicationUser user = _userManager.FindById(model.userid);
            ApplicationUser user = await _userManager.FindByIdAsync(models.UserId);
            if (user != null)
            {

                if (await _userManager.IsInRoleAsync(user,"Admin, Doctor"))
                {
                    user.Email = models.EmailAddress;
                    user.FirstName = models.FirstName;
                    user.SurName = models.SurName;
                    user.OtherName = models.OtherName;
                }
                user.Address = models.Address;
                user.EntityStatus = models.Status;
                user.Sex = models.Sex;
                user.MobileNo = models.MobileNo;
                user.ProfilePicture = "/uploads/" + fileName;
                
                
                
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<Doctor> GetDoctorAsync(int? id)
        {
            var doctor = await _context.Doctor.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            return doctor;
        }

        public async Task<Doctor> GetDoctorWithoutIdAsync()
        {
            var user = _contextAccessor.HttpContext.User?.Identity?.Name;

            var doctor = await _context.Doctor.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == user);
            return doctor;
        }

        public async Task<List<Doctor>> ListDoctorAsync()
        {
            var items = await _context.Doctor.Include(x => x.User).Include(x => x.User).ToListAsync();
            return items;
        }
    }
}
