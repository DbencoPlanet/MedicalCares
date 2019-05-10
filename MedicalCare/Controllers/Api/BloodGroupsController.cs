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
    [Route("api/BloodGroups")]
    public class BloodGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BloodGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BloodGroups
        [HttpGet]
        public IEnumerable<BloodGroup> GetBloodGroup()
        {
            return _context.BloodGroup;
        }

        // GET: api/BloodGroups/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBloodGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bloodGroup = await _context.BloodGroup.SingleOrDefaultAsync(m => m.Id == id);

            if (bloodGroup == null)
            {
                return NotFound();
            }

            return Ok(bloodGroup);
        }

        // PUT: api/BloodGroups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBloodGroup([FromRoute] int id, [FromBody] BloodGroup bloodGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bloodGroup.Id)
            {
                return BadRequest();
            }

            _context.Entry(bloodGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BloodGroupExists(id))
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

        // POST: api/BloodGroups
        [HttpPost]
        public async Task<IActionResult> PostBloodGroup([FromBody] BloodGroup bloodGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BloodGroup.Add(bloodGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBloodGroup", new { id = bloodGroup.Id }, bloodGroup);
        }

        // DELETE: api/BloodGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodGroup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bloodGroup = await _context.BloodGroup.SingleOrDefaultAsync(m => m.Id == id);
            if (bloodGroup == null)
            {
                return NotFound();
            }

            _context.BloodGroup.Remove(bloodGroup);
            await _context.SaveChangesAsync();

            return Ok(bloodGroup);
        }

        private bool BloodGroupExists(int id)
        {
            return _context.BloodGroup.Any(e => e.Id == id);
        }
    }
}