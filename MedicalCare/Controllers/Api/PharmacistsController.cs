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
using Microsoft.AspNetCore.Hosting;
using MedicalCare.Areas.Admin.Data.Services;
using MedicalCare.Areas.Admin.Data.IServices;
using MedicalCare.Models;
using Microsoft.AspNetCore.Identity;
using MedicalCare.Models.Dtos;
using MedicalCare.Models.AccountViewModels;

namespace MedicalCare.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Pharmacists")]
    public class PharmacistsController : Controller
    {
        private readonly IDotnetdesk _dotnetdesk;
        private readonly IHostingEnvironment _env;
        private readonly IUserManagerService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPharmacistService _pharmacistService;


        public PharmacistsController(UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager,
           ApplicationDbContext context,
           SignInManager<ApplicationUser> signInManager,
           IDotnetdesk dotnetdesk,
            IHostingEnvironment env,
            UserManagerService userService,
            PharmacistService pharmacistService
          )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
            _dotnetdesk = dotnetdesk;
            _env = env;
            _userService = userService;
            _pharmacistService = pharmacistService;

        }

        // GET: api/Pharmacists
        [HttpGet]
        public IEnumerable<Pharmacist> GetPharmacists()
        {
            return _context.Pharmacists;
        }

        // GET: api/Pharmacists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPharmacist([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pharmacist = await _context.Pharmacists.SingleOrDefaultAsync(m => m.Id == id);

            if (pharmacist == null)
            {
                return NotFound();
            }

            return Ok(pharmacist);
        }

        // GET: api/Pharmacists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> PutPharmacist(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await _pharmacistService.GetPharmacistAsync(id);
            if (user == null)
            {
                return BadRequest();
            }



            return View(user);
        }

        // PUT: api/Pharmacists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPharmacist([FromRoute] int id, [FromBody] PharmacistDto pharmacist, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _pharmacistService.EditPharmacistAsync(pharmacist, files);
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



            return View(pharmacist);
        }



        // POST: api/Patients
        [HttpPost]
        public async Task<IActionResult> PostPatient([FromBody] RegisterViewModel pharmacist, List<IFormFile> files)
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
                    succed = await _userService.NewPharmacistAsync(pharmacist, files);
                    if (succed == "true")
                    {
                        //TempData["success"] = "Doctor with Name <i> " + doctor.Fullname + "</i> Added Successfully";
                        //return RedirectToAction("PostDoctor");
                        return Json(new { success = true, message = "Pharmacist with Name <i> " + pharmacist.Fullname + "</i> Added Successfully." });
                    }

                    else
                    {
                        //TempData["error1"] = succed;
                        return Json(new { success = false, message = "Pharmacist Account Creation Failed." });
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



        // DELETE: api/Pharmacists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePharmacist([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pharmacist = await _context.Pharmacists.SingleOrDefaultAsync(m => m.Id == id);
            if (pharmacist == null)
            {
                return NotFound();
            }

            _context.Pharmacists.Remove(pharmacist);
            await _context.SaveChangesAsync();

            return Ok(pharmacist);
        }

        private bool PharmacistExists(int id)
        {
            return _context.Pharmacists.Any(e => e.Id == id);
        }
    }
}