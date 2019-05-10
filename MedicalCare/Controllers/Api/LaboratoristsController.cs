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
using MedicalCare.Areas.Admin.Data.IServices;
using MedicalCare.Models;
using Microsoft.AspNetCore.Identity;
using MedicalCare.Areas.Admin.Data.Services;
using MedicalCare.Models.Dtos;
using MedicalCare.Models.AccountViewModels;

namespace MedicalCare.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Laboratorists")]
    public class LaboratoristsController : Controller
    {
        private readonly IDotnetdesk _dotnetdesk;
        private readonly IHostingEnvironment _env;
        private readonly IUserManagerService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILaboratoristService _laboratoristService;


        public LaboratoristsController(UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager,
           ApplicationDbContext context,
           SignInManager<ApplicationUser> signInManager,
           IDotnetdesk dotnetdesk,
            IHostingEnvironment env,
            UserManagerService userService,
            LaboratoristService laboratoristService
          )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
            _dotnetdesk = dotnetdesk;
            _env = env;
            _userService = userService;
            _laboratoristService = laboratoristService;

        }

        // GET: api/Laboratorists
        [HttpGet]
        public IEnumerable<Laboratorist> GetLaboratorists()
        {
            return _context.Laboratorists;
        }

        // GET: api/Laboratorists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLaboratorist([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var laboratorist = await _context.Laboratorists.SingleOrDefaultAsync(m => m.Id == id);

            if (laboratorist == null)
            {
                return NotFound();
            }

            return Ok(laboratorist);
        }

        // GET: api/Laboratorists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> PutLaboratorist(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await _laboratoristService.GetLaboratoristAsync(id);
            if (user == null)
            {
                return BadRequest();
            }



            return View(user);
        }

        // PUT: api/Laboratorists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLaboratorist([FromRoute] int id, [FromBody] LaboratoristDto laboratorist, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _laboratoristService.EditLaboratoristAsync(laboratorist, files);
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



            return View(laboratorist);
        }



        // POST: api/Laboratorists
        [HttpPost]
        public async Task<IActionResult> PostAccountant([FromBody] RegisterViewModel laboratorist, List<IFormFile> files)
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
                    succed = await _userService.NewLaboratoristAsync(laboratorist, files);
                    if (succed == "true")
                    {
                        //TempData["success"] = "Doctor with Name <i> " + doctor.Fullname + "</i> Added Successfully";
                        //return RedirectToAction("PostDoctor");
                        return Json(new { success = true, message = "Laboratorist with Name <i> " + laboratorist.Fullname + "</i> Added Successfully." });
                    }

                    else
                    {
                        //TempData["error1"] = succed;
                        return Json(new { success = false, message = "Laboratorist Account Creation Failed." });
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
        // DELETE: api/Laboratorists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLaboratorist([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var laboratorist = await _context.Laboratorists.SingleOrDefaultAsync(m => m.Id == id);
            if (laboratorist == null)
            {
                return NotFound();
            }

            _context.Laboratorists.Remove(laboratorist);
            await _context.SaveChangesAsync();

            return Ok(laboratorist);
        }

        private bool LaboratoristExists(int id)
        {
            return _context.Laboratorists.Any(e => e.Id == id);
        }
    }
}