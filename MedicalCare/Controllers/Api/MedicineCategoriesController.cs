using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalCare.Data;
using MedicalCare.Models.Entities;

namespace MedicalCare.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/MedicineCategories")]
    public class MedicineCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedicineCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MedicineCategories
        [HttpGet]
        public IEnumerable<MedicineCategory> GetMedicineCategory()
        {
            return _context.MedicineCategory;
        }

        // GET: api/MedicineCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicineCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medicineCategory = await _context.MedicineCategory.SingleOrDefaultAsync(m => m.Id == id);

            if (medicineCategory == null)
            {
                return NotFound();
            }

            return Ok(medicineCategory);
        }

        // PUT: api/MedicineCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicineCategory([FromRoute] int id, [FromBody] MedicineCategory medicineCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicineCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(medicineCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MedicineCategories
        [HttpPost]
        public async Task<IActionResult> PostMedicineCategory([FromBody] MedicineCategory medicineCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MedicineCategory.Add(medicineCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicineCategory", new { id = medicineCategory.Id }, medicineCategory);
        }

        // DELETE: api/MedicineCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicineCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medicineCategory = await _context.MedicineCategory.SingleOrDefaultAsync(m => m.Id == id);
            if (medicineCategory == null)
            {
                return NotFound();
            }

            _context.MedicineCategory.Remove(medicineCategory);
            await _context.SaveChangesAsync();

            return Ok(medicineCategory);
        }

        private bool MedicineCategoryExists(int id)
        {
            return _context.MedicineCategory.Any(e => e.Id == id);
        }
    }
}