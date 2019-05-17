using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicalCare.Data;
using MedicalCare.Models.Entities;

namespace MedicalCare.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SettingController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddEdit(int? id)
        {
            if (id == null)
            {
                Setting sett = new Setting();
                return View(sett);
            }
            else
            {
                return View(_context.Settings.Where(x => x.Id.Equals(id)).FirstOrDefault());
            }

        }

        public IActionResult Detail(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            Setting sett = _context.Settings.Where(x => x.Id.Equals(Id)).FirstOrDefault();
            return View(sett);
        }

    }
}