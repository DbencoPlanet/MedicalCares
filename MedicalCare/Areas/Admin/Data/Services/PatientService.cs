using MedicalCare.Areas.Admin.Data.IServices;
using MedicalCare.Data;
using MedicalCare.Models;
using MedicalCare.Models.Dtos;
using MedicalCare.Models.Entities;
using MedicalCare.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCare.Areas.Admin.Data.Services
{
    public class PatientService : IPatientService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDotnetdesk _dotnetdesk;
        private readonly IHostingEnvironment _env;


        public PatientService(UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager,
           ApplicationDbContext context,
           SignInManager<ApplicationUser> signInManager,
           IHttpContextAccessor contextAccessor,
             IDotnetdesk dotnetdesk,
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

        public async Task EditPatientAsync(PatientDto models, List<IFormFile> files)
        {
            var fileName = await _dotnetdesk.UploadFile(files, _env);

            Patient pat = await _context.Patients.FindAsync(models.Id);
            pat.Address = models.Address;
            pat.EmailAddress = models.EmailAddress;
            pat.FirstName = models.FirstName;
            pat.OtherName = models.OtherName;
            pat.SurName = models.SurName;
            pat.MobileNo = models.MobileNo;
            pat.PhoneNo = models.PhoneNo;
            pat.ProfilePicture = "/uploads/" + fileName;
            pat.Sex = models.Sex;
            pat.Status = models.Status;
            pat.BloodGroupId = models.BloodGroupId;
            pat.DOB = models.DOB;


            _context.Entry(pat).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //   ApplicationUser user = _userManager.FindById(model.userid);
            ApplicationUser user = await _userManager.FindByIdAsync(models.UserId);
            if (user != null)
            {


                user.Email = models.EmailAddress;
                user.FirstName = models.FirstName;
                user.SurName = models.SurName;
                user.OtherName = models.OtherName;

                user.Address = models.Address;
                user.EntityStatus = models.Status;
                user.Sex = models.Sex;
                user.MobileNo = models.MobileNo;
                user.ProfilePicture = "/uploads/" + fileName;



                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<Patient> GetPatientAsync(int? id)
        {
            var pat = await _context.Patients.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            return pat;
        }

        public async Task<Patient> GetPatientWithoutIdAsync()
        {
            var user = _contextAccessor.HttpContext.User?.Identity?.Name;

            var pat = await _context.Patients.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == user);
            return pat;
        }

        public async Task<List<Patient>> ListPatientAsync()
        {
            var items = await _context.Patients.Include(x => x.User).Include(x => x.User).ToListAsync();
            return items;
        }
    }
}
