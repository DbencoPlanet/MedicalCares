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
    [Route("api/Enquiries")]
    public class EnquiriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnquiriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Enquiries
        [HttpGet]
        public IEnumerable<Enquiries> GetEnquiries()
        {
            return _context.Enquiries;
        }

        // GET: api/Enquiries/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnquiries([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enquiries = await _context.Enquiries.SingleOrDefaultAsync(m => m.Id == id);

            if (enquiries == null)
            {
                return NotFound();
            }

            return Ok(enquiries);
        }

        // PUT: api/Enquiries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnquiries([FromRoute] int id, [FromBody] Enquiries enquiries)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != enquiries.Id)
            {
                return BadRequest();
            }

            _context.Entry(enquiries).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnquiriesExists(id))
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

        // POST: api/Enquiries
        [HttpPost]
        public async Task<IActionResult> PostEnquiries([FromBody] Enquiries enquiries)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Enquiries.Add(enquiries);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnquiries", new { id = enquiries.Id }, enquiries);
        }

        // DELETE: api/Enquiries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnquiries([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enquiries = await _context.Enquiries.SingleOrDefaultAsync(m => m.Id == id);
            if (enquiries == null)
            {
                return NotFound();
            }

            _context.Enquiries.Remove(enquiries);
            await _context.SaveChangesAsync();

            return Ok(enquiries);
        }

        private bool EnquiriesExists(int id)
        {
            return _context.Enquiries.Any(e => e.Id == id);
        }
    }
}