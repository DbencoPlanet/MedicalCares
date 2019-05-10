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
    public class LaboratoristService : ILaboratoristService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDotnetdesk _dotnetdesk;
        private readonly IHostingEnvironment _env;


        public LaboratoristService(UserManager<ApplicationUser> userManager,
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

        public async Task EditLaboratoristAsync(LaboratoristDto models, List<IFormFile> files)
        {
            var fileName = await _dotnetdesk.UploadFile(files, _env);

            Laboratorist lab = await _context.Laboratorists.FindAsync(models.Id);
            lab.Address = models.Address;
            lab.EmailAddress = models.EmailAddress;
            lab.FirstName = models.FirstName;
            lab.OtherName = models.OtherName;
            lab.SurName = models.SurName;
            lab.MobileNo = models.MobileNo;
            lab.PhoneNo = models.PhoneNo;
            lab.ProfilePicture = "/uploads/" + fileName;
            lab.Sex = models.Sex;
            lab.Status = models.Status;



            _context.Entry(lab).State = EntityState.Modified;
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

        public async Task<Laboratorist> GetLaboratoristAsync(int? id)
        {
            var lab = await _context.Laboratorists.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            return lab;
        }

        public async Task<Laboratorist> GetLaboratoristWithoutIdAsync()
        {
            var user = _contextAccessor.HttpContext.User?.Identity?.Name;

            var lab = await _context.Laboratorists.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == user);
            return lab;
        }

        public async Task<List<Laboratorist>> ListLaboratoristAsync()
        {
            var items = await _context.Laboratorists.Include(x => x.User).Include(x => x.User).ToListAsync();
            return items;
        }
    }
}
