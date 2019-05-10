using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalCare.Data;
using MedicalCare.Models.Entities;
using MedicalCare.Services;
using MedicalCare.Models;
using MedicalCare.Models.AccountViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using MedicalCare.Areas.Admin.Data.IServices;
using MedicalCare.Areas.Admin.Data.Services;
using MedicalCare.Models.Dtos;

namespace MedicalCare.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Accountants")]
    public class AccountantsController : Controller
    {

        private readonly IDotnetdesk _dotnetdesk;
        private readonly IHostingEnvironment _env;
        private readonly IUserManagerService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAccountantsService _accountantService;


        public AccountantsController(UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager,
           ApplicationDbContext context,
           SignInManager<ApplicationUser> signInManager,
           IDotnetdesk dotnetdesk,
            IHostingEnvironment env,
            UserManagerService userService,
            AccountantsService accountantService
          )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
            _dotnetdesk = dotnetdesk;
            _env = env;
            _userService = userService;
            _accountantService = accountantService;

        }


        public AccountantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Accountants
        [HttpGet]
        public IEnumerable<Accountants> GetAccountants()
        {
            return _context.Accountants;
        }

        // GET: api/Accountants/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accountant = await _context.Accountants.SingleOrDefaultAsync(m => m.Id == id);

            if (accountant == null)
            {
                return NotFound();
            }

            return Ok(accountant);
        }

        // GET: api/Accountant/5
        [HttpGet("{id}")]
        public async Task<IActionResult> PutAccountant(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await _accountantService.GetAccountantAsync(id);
            if (user == null)
            {
                return BadRequest();
            }

        

            return View(user);
        }

        // PUT: api/Doctors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountant([FromRoute] int id, [FromBody] AccountantDto accountant, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _accountantService.EditAccountantAsync(accountant, files);
                    //TempData["success"] = "Update Successful.";
                    return Json(new { success = true, message = "Update Unsuccessful." });
                    //return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //TempData["error"] = "Update Unsuccessful, (" + e.ToString() + ")";
                    return Json(new { success = false, message = ex.Message });
                }



            }

           

            return View(accountant);
        }



        // POST: api/Accountants
        [HttpPost]
        public async Task<IActionResult> PostAccountant([FromBody] RegisterViewModel accountant, List<IFormFile> files)
        {
           

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ee = "";
            if (ModelState.IsValid)
            {
                try
                {
                    string succed;
                    succed = await _userService.NewAccountantAsync(accountant, files);
                    if (succed == "true")
                    {
                        //TempData["success"] = "Doctor with Name <i> " + doctor.Fullname + "</i> Added Successfully";
                        //return RedirectToAction("PostDoctor");
                        return Json(new { success = true, message = "Accountant with Name <i> " + accountant.Fullname + "</i> Added Successfully." });
                    }

                    else
                    {
                        //TempData["error1"] = succed;
                        return Json(new { success = false, message = "Accountant Account Creation Failed." });
                    }
                }

                catch (Exception ex)
                {
                    //ee = e.ToString();
                    return Json(new { success = false, message = ex.Message });
                }


            }

       
            return View();




        }

        // DELETE: api/Accountants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accountant = await _context.Accountants.SingleOrDefaultAsync(m => m.Id == id);
            if (accountant == null)
            {
                return NotFound();
            }

            _context.Accountants.Remove(accountant);
            await _context.SaveChangesAsync();

            return Ok(accountant);
        }

        private bool AccountantExists(int id)
        {
            return _context.Accountants.Any(e => e.Id == id);
        }
    }
}