using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalCare.Data;
using MedicalCare.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using MedicalCare.Areas.Admin.Data.Services;
using MedicalCare.Services;
using MedicalCare.Areas.Admin.Data.IServices;
using Microsoft.AspNetCore.Identity;
using MedicalCare.Models;
using MedicalCare.Models.Dtos;
using MedicalCare.Models.AccountViewModels;

namespace MedicalCare.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Nurses")]
    public class NursesController : Controller
    {
        private readonly IDotnetdesk _dotnetdesk;
        private readonly IHostingEnvironment _env;
        private readonly IUserManagerService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly INurseService _nurseService;


        public NursesController(UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager,
           ApplicationDbContext context,
           SignInManager<ApplicationUser> signInManager,
           IDotnetdesk dotnetdesk,
            IHostingEnvironment env,
            UserManagerService userService,
            NurseService nurseService
          )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
            _dotnetdesk = dotnetdesk;
            _env = env;
            _userService = userService;
            _nurseService = nurseService;

        }

        // GET: api/Nurses
        [HttpGet]
        public IEnumerable<Nurse> GetNurse()
        {
            return _context.Nurse;
        }

        // GET: api/Nurses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNurse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nurse = await _context.Nurse.SingleOrDefaultAsync(m => m.Id == id);

            if (nurse == null)
            {
                return NotFound();
            }

            return Ok(nurse);
        }

        // GET: api/Nurses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> PutNurse(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await _nurseService.GetNurseAsync(id);
            if (user == null)
            {
                return BadRequest();
            }



            return View(user);
        }

        // PUT: api/Nurses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNurse([FromRoute] int id, [FromBody] NurseDto nurse, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _nurseService.EditNurseAsync(nurse, files);
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



            return View(nurse);
        }



        // POST: api/Nurses
        [HttpPost]
        public async Task<IActionResult> PostNurse([FromBody] RegisterViewModel nurse, List<IFormFile> files)
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
                    succed = await _userService.NewNurseAsync(nurse, files);
                    if (succed == "true")
                    {
                        //TempData["success"] = "Doctor with Name <i> " + doctor.Fullname + "</i> Added Successfully";
                        //return RedirectToAction("PostDoctor");
                        return Json(new { success = true, message = "Nurse with Name <i> " + nurse.Fullname + "</i> Added Successfully." });
                    }

                    else
                    {
                        //TempData["error1"] = succed;
                        return Json(new { success = false, message = "Nurse Account Creation Failed." });
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

        // DELETE: api/Nurses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNurse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nurse = await _context.Nurse.SingleOrDefaultAsync(m => m.Id == id);
            if (nurse == null)
            {
                return NotFound();
            }

            _context.Nurse.Remove(nurse);
            await _context.SaveChangesAsync();

            return Ok(nurse);
        }

        private bool NurseExists(int id)
        {
            return _context.Nurse.Any(e => e.Id == id);
        }
    }
}