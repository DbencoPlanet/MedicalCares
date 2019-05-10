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
    [Route("api/BedCategories")]
    public class BedCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BedCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BedCategories
        [HttpGet]
        public IEnumerable<BedCategory> GetBedCategory()
        {
            return _context.BedCategory;
        }

        // GET: api/BedCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBedCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bedCategory = await _context.BedCategory.SingleOrDefaultAsync(m => m.Id == id);

            if (bedCategory == null)
            {
                return NotFound();
            }

            return Ok(bedCategory);
        }

        // PUT: api/BedCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBedCategory([FromRoute] int id, [FromBody] BedCategory bedCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bedCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(bedCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BedCategoryExists(id))
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

        // POST: api/BedCategories
        [HttpPost]
        public async Task<IActionResult> PostBedCategory([FromBody] BedCategory bedCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BedCategory.Add(bedCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBedCategory", new { id = bedCategory.Id }, bedCategory);
        }

        // DELETE: api/BedCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBedCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bedCategory = await _context.BedCategory.SingleOrDefaultAsync(m => m.Id == id);
            if (bedCategory == null)
            {
                return NotFound();
            }

            _context.BedCategory.Remove(bedCategory);
            await _context.SaveChangesAsync();

            return Ok(bedCategory);
        }

        private bool BedCategoryExists(int id)
        {
            return _context.BedCategory.Any(e => e.Id == id);
        }
    }
}