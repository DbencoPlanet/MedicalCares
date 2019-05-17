using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicalCare.Models;
using Microsoft.AspNetCore.Identity;
using MedicalCare.Services;
using Microsoft.Extensions.Logging;
using MedicalCare.Data;
using MedicalCare.Models.AccountViewModels;

namespace MedicalCare.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;


        public HomeController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            ApplicationDbContext context,
             RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
        }


        public IActionResult Index()
        {
            //    var user1 = _context.Users;

            //    var userid = new ApplicationUser();


            //    if (user1.Count() < 1)
            //    {
            //        return RedirectToAction("CreateAccount", "AdminUser", new { area = "SuperUser" });
            //    }
            //    else
            //    {
            //        if (User.Identity.IsAuthenticated)
            //        {

            //            if (await _userManager.IsInRoleAsync(userid, "Admin") || await _userManager.IsInRoleAsync(userid, "SuperAdmin"))
            //            {
            //                return RedirectToAction("Index", "DashBoard", new { area = "Admin" });
            //            }
            //            else if (await _userManager.IsInRoleAsync(userid, "Doctor"))
            //            {
            //                return RedirectToAction("Index", "DashBoard", new { area = "Doctor" });
            //            }
            //            else if (await _userManager.IsInRoleAsync(userid, "Patient"))
            //            {
            //                return RedirectToAction("Index", "DashBoard", new { area = "Patient" });
            //            }
            //            else if (await _userManager.IsInRoleAsync(userid, "Nurse"))
            //            {
            //                return RedirectToAction("Index", "DashBoard", new { area = "Nurse" });
            //            }
            //            else if (await _userManager.IsInRoleAsync(userid, "Pharmacist"))
            //            {
            //                return RedirectToAction("Index", "DashBoard", new { area = "Pharmacist" });
            //            }

            //            else if (await _userManager.IsInRoleAsync(userid, "Receptionist"))
            //            {
            //                return RedirectToAction("Index", "DashBoard", new { area = "Receptionist" });
            //            }
            //            else if (await _userManager.IsInRoleAsync(userid, "Laboratorist"))
            //            {
            //                return RedirectToAction("Index", "DashBoard", new { area = "Laboratorist" });
            //            }

            //            else if (await _userManager.IsInRoleAsync(userid, "Accountant"))
            //            {
            //                return RedirectToAction("Index", "DashBoard", new { area = "Accountant" });
            //            }
            //            else if (await _userManager.IsInRoleAsync(userid, "Employee"))
            //            {
            //                return RedirectToAction("Index", "DashBoard", new { area = "Accountant" });
            //            }
            //            else
            //            {

            //                return RedirectToAction("Login", "Account", new { area = "" });
            //            }
            //        }
            //    }
            //    return RedirectToAction("Login", "Account", new { area = "" });
            //}
            return View();
        }




        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";

        //    return View();
        //}

        //public IActionResult Contact()
        //{
        //    ViewData["Message"] = "Your contact page.";

        //    return View();
        //}

        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}


