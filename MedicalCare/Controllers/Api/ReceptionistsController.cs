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
using Microsoft.AspNetCore.Identity;
using MedicalCare.Areas.Admin.Data.IServices;
using MedicalCare.Models;
using MedicalCare.Areas.Admin.Data.Services;
using MedicalCare.Models.Dtos;
using MedicalCare.Models.AccountViewModels;

namespace MedicalCare.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Receptionists")]
    public class ReceptionistsController : Controller
    {
        private readonly IDotnetdesk _dotnetdesk;
        private readonly IHostingEnvironment _env;
        private readonly IUserManagerService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IReceptionistService _receptionistService;


        public ReceptionistsController(UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager,
           ApplicationDbContext context,
           SignInManager<ApplicationUser> signInManager,
           IDotnetdesk dotnetdesk,
            IHostingEnvironment env,
            UserManagerService userService,
            ReceptionistService receptionistService
          )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
            _dotnetdesk = dotnetdesk;
            _env = env;
            _userService = userService;
            _receptionistService = receptionistService;

        }

        // GET: api/Receptionists
        [HttpGet]
        public IEnumerable<Receptionist> GetReceptionists()
        {
            return _context.Receptionists;
        }

        // GET: api/Receptionists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReceptionist([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receptionist = await _context.Receptionists.SingleOrDefaultAsync(m => m.Id == id);

            if (receptionist == null)
            {
                return NotFound();
            }

            return Ok(receptionist);
        }

        // GET: api/Receptionists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> PutReceptionist(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await _receptionistService.GetReceptionistAsync(id);
            if (user == null)
            {
                return BadRequest();
            }



            return View(user);
        }

        // PUT: api/Receptionists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceptionist([FromRoute] int id, [FromBody] ReceptionistDto receptionist, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _receptionistService.EditReceptionistAsync(receptionist, files);
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



            return View(receptionist);
        }



        // POST: api/Patients
        [HttpPost]
        public async Task<IActionResult> PostReceptionist([FromBody] RegisterViewModel receptionist, List<IFormFile> files)
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
                    succed = await _userService.NewReceptionistAsync(receptionist, files);
                    if (succed == "true")
                    {
                        //TempData["success"] = "Doctor with Name <i> " + doctor.Fullname + "</i> Added Successfully";
                        //return RedirectToAction("PostDoctor");
                        return Json(new { success = true, message = "Receptionist with Name <i> " + receptionist.Fullname + "</i> Added Successfully." });
                    }

                    else
                    {
                        //TempData["error1"] = succed;
                        return Json(new { success = false, message = "Receptionist Account Creation Failed." });
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


        // DELETE: api/Receptionists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceptionist([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receptionist = await _context.Receptionists.SingleOrDefaultAsync(m => m.Id == id);
            if (receptionist == null)
            {
                return NotFound();
            }

            _context.Receptionists.Remove(receptionist);
            await _context.SaveChangesAsync();

            return Ok(receptionist);
        }

        private bool ReceptionistExists(int id)
        {
            return _context.Receptionists.Any(e => e.Id == id);
        }
    }
}