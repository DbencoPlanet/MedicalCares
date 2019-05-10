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
    public class ReceptionistService : IReceptionistService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IDotnetdesk _dotnetdesk;
        private readonly IHostingEnvironment _env;


        public ReceptionistService(UserManager<ApplicationUser> userManager,
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

        public async Task EditReceptionistAsync(ReceptionistDto models, List<IFormFile> files)
        {
            var fileName = await _dotnetdesk.UploadFile(files, _env);

            Receptionist pha = await _context.Receptionists.FindAsync(models.Id);
            pha.Address = models.Address;
            pha.EmailAddress = models.EmailAddress;
            pha.FirstName = models.FirstName;
            pha.OtherName = models.OtherName;
            pha.SurName = models.SurName;
            pha.MobileNo = models.MobileNo;
            pha.PhoneNo = models.PhoneNo;
            pha.ProfilePicture = "/uploads/" + fileName;
            pha.Sex = models.Sex;
            pha.Status = models.Status;



            _context.Entry(pha).State = EntityState.Modified;
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

        public async Task<Receptionist> GetReceptionistAsync(int? id)
        {
            var pha = await _context.Receptionists.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            return pha;
        }

        public async Task<Receptionist> GetReceptionistWithoutIdAsync()
        {
            var user = _contextAccessor.HttpContext.User?.Identity?.Name;

            var pha = await _context.Receptionists.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == user);
            return pha;
        }

        public async Task<List<Receptionist>> ListReceptionistAsync()
        {
            var items = await _context.Receptionists.Include(x => x.User).Include(x => x.User).ToListAsync();
            return items;
        }
    }
}
