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
    [Route("api/BedAllotments")]
    public class BedAllotmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BedAllotmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BedAllotments
        [HttpGet]
        public IEnumerable<BedAllotment> GetBedAllotments()
        {
            return _context.BedAllotments;
        }

        // GET: api/BedAllotments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBedAllotment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bedAllotment = await _context.BedAllotments.SingleOrDefaultAsync(m => m.Id == id);

            if (bedAllotment == null)
            {
                return NotFound();
            }

            return Ok(bedAllotment);
        }

        // PUT: api/BedAllotments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBedAllotment([FromRoute] int id, [FromBody] BedAllotment bedAllotment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bedAllotment.Id)
            {
                return BadRequest();
            }

            _context.Entry(bedAllotment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BedAllotmentExists(id))
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

        // POST: api/BedAllotments
        [HttpPost]
        public async Task<IActionResult> PostBedAllotment([FromBody] BedAllotment bedAllotment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BedAllotments.Add(bedAllotment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBedAllotment", new { id = bedAllotment.Id }, bedAllotment);
        }

        // DELETE: api/BedAllotments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBedAllotment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bedAllotment = await _context.BedAllotments.SingleOrDefaultAsync(m => m.Id == id);
            if (bedAllotment == null)
            {
                return NotFound();
            }

            _context.BedAllotments.Remove(bedAllotment);
            await _context.SaveChangesAsync();

            return Ok(bedAllotment);
        }

        private bool BedAllotmentExists(int id)
        {
            return _context.BedAllotments.Any(e => e.Id == id);
        }
    }
}