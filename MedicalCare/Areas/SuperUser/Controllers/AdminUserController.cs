using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalCare.Data;
using MedicalCare.Models;
using MedicalCare.Models.AccountViewModels;
using MedicalCare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCare.Areas.SuperUser.Controllers
{
    public class AdminUserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
 


        public AdminUserController(UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager,
           ApplicationDbContext context,
           SignInManager<ApplicationUser> signInManager
         
         
          
          )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
           

        }
        public IActionResult Index(RegisterViewModel model)
        {
            return View();
        }


        // POST: /Account/Register
        [Area("SuperUser")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAccount(RegisterViewModel model)
        {
            //model.Username = "SuperAdmin";
            model.Email = "superadmin@super.com";
            var user = new ApplicationUser { Email = model.Email};
            model.Password = "super@123";

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var role = new IdentityRole("SuperAdmin");
                var role1 = new IdentityRole("Admin");
                var role2 = new IdentityRole("Doctor");
                var role3 = new IdentityRole("Nurse");
                var role4 = new IdentityRole("Accountant");
                var role5 = new IdentityRole("Laboratorist");
                var role6 = new IdentityRole("Patient");
                var role7 = new IdentityRole("Pharmacist");
                var role8 = new IdentityRole("Receptionist");
                var role9 = new IdentityRole("Employee");
                await _roleManager.CreateAsync(role);
                await _roleManager.CreateAsync(role1);
                await _roleManager.CreateAsync(role2);
                await _roleManager.CreateAsync(role3);
                await _roleManager.CreateAsync(role4);
                await _roleManager.CreateAsync(role5);
                await _roleManager.CreateAsync(role6);
                await _roleManager.CreateAsync(role7);
                await _roleManager.CreateAsync(role8);
                await _roleManager.CreateAsync(role9);

                await _userManager.AddToRoleAsync(user, "SuperAdmin");
                ////
                ///

                ///
                await _signInManager.SignInAsync(user, isPersistent: false);

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                return RedirectToAction("CreateAdmin");
            }
            TempData["error"] = "Contact Administrator";
            return View();

        }

         public async Task<ActionResult> CreateAdmin(RegisterViewModel model)
        {

            //model.Username = "Admin";
            model.Email = "admin@admin.com";
            var user = new ApplicationUser { Email = model.Email};
            model.Password = "Admin@123";

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                await _userManager.AddToRoleAsync(user, "Admin");
                ////
                ///
               
                return RedirectToAction("Create", "Settings", new { area = "Admin" });
            }
            return View();

        }
    }
}