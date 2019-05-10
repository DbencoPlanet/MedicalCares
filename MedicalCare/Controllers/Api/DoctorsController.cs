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
using MedicalCare.Models;
using MedicalCare.Models.AccountViewModels;
using MedicalCare.Areas.Admin.Data.IServices;
using MedicalCare.Areas.Admin.Data.Services;
using MedicalCare.Models.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalCare.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Doctors")]
    public class DoctorsController : Controller
    {
        private readonly IDotnetdesk _dotnetdesk;
        private readonly IHostingEnvironment _env;
        private readonly IUserManagerService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IDoctorService _doctorService;


        public DoctorsController(UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager,
           ApplicationDbContext context,
           SignInManager<ApplicationUser> signInManager,
           IDotnetdesk dotnetdesk,
            IHostingEnvironment env,
            UserManagerService userService,
            DoctorService doctorService
          )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
            _dotnetdesk = dotnetdesk;
            _env = env;
            _userService = userService;
            _doctorService = doctorService;

        }

        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Doctors
        [HttpGet]
        public IEnumerable<Doctor> GetDoctor()
        {
            return _context.Doctor;
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = await _context.Doctor.SingleOrDefaultAsync(m => m.Id == id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }


        // POST: api/Doctor
        [HttpPost]
        public async Task<IActionResult> PostAccountant([FromBody] RegisterViewModel doctor, List<IFormFile> files, int BloodGpId, string designation, int deptId, string biography, string specialist, string education)
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
                    succed = await _userService.NewDoctorAsync(doctor, files, BloodGpId, designation, deptId, biography,specialist,education);
                    if (succed == "true")
                    {
                        //TempData["success"] = "Doctor with Name <i> " + doctor.Fullname + "</i> Added Successfully";
                        //return RedirectToAction("PostDoctor");
                        return Json(new { success = true, message = "Doctor with Name <i> " + doctor.Fullname + "</i> Added Successfully." });
                    }

                    else
                    {
                        //TempData["error1"] = succed;
                        return Json(new { success = false, message = "Doctor Account Creation Failed." });
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



        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> PutDoctor(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await _doctorService.GetDoctorAsync(id);
            if (user == null)
            {
                return BadRequest();
            }

            ViewBag.DeptId = new SelectList(_context.Departments, "Id", "DeptName");
            ViewBag.BloodGpId = new SelectList(_context.BloodGroup, "Id", "Name");

            return View(user);
        }

        // PUT: api/Doctors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor([FromRoute] int id,[FromBody] DoctorDto doctor, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _doctorService.EditDoctorAsync(doctor,files);
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

          
             ViewBag.DeptId = new SelectList(_context.Departments, "Id", "DeptName");
            ViewBag.BloodGpId = new SelectList(_context.BloodGroup, "Id", "Name");

            return View(doctor);
        }



         


        // GET: api/Doctors
        [HttpGet]
        public IActionResult PostDoctor()
        {


            ViewBag.DeptId = new SelectList(_context.Departments, "Id", "DeptName");
            ViewBag.BloodGpId = new SelectList(_context.BloodGroup, "Id", "Name");
            return View();

        }

        // POST: api/Doctors
        [HttpPost]
        public async Task<IActionResult> PostDoctor([FromBody] RegisterViewModel doctor, List<IFormFile> files, int BloodGpId, string designation, int deptId, string biography, string specialist, string education, int id)
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
                    succed = await _userService.NewDoctorAsync(doctor, files, BloodGpId, designation, deptId, biography, specialist, education);
                    if (succed == "true")
                    {
                        //TempData["success"] = "Doctor with Name <i> " + doctor.Fullname + "</i> Added Successfully";
                        //return RedirectToAction("PostDoctor");
                        return Json(new { success = true, message = "Doctor with Name <i> " + doctor.Fullname + "</i> Added Successfully."});
                    }

                    else
                    {
                        //TempData["error1"] = succed;
                        return Json(new { success = false, message = "Doctor Account Creation Failed." });
                    }
                }

                catch (Exception ex)
                {
                    //ee = e.ToString();
                    return Json(new { success = false, message = ex.Message });
                }

              
                //await _context.SaveChangesAsync();

                //var doctor1 = new Doctor();

                //return CreatedAtAction("GetDoctor", new { id = doctor1.Id}, doctor);
            }

            //var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            //TempData["error"] = "Creation of new doctor not successful" + ee;

            ViewBag.DeptId = new SelectList(_context.Departments, "Id", "DeptName");
            ViewBag.BloodGpId = new SelectList(_context.BloodGroup, "Id", "Name");

            return View();

        }

        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = await _context.Doctor.SingleOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctor.Remove(doctor);
            await _context.SaveChangesAsync();

            return Ok(doctor);
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctor.Any(e => e.Id == id);
        }
    }
}