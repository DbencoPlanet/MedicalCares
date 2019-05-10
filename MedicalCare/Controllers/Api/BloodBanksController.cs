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
    [Route("api/BloodBanks")]
    public class BloodBanksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BloodBanksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BloodBanks
        [HttpGet]
        public IEnumerable<BloodBank> GetBloodBank()
        {
            return _context.BloodBank;
        }

        // GET: api/BloodBanks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBloodBank([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bloodBank = await _context.BloodBank.SingleOrDefaultAsync(m => m.Id == id);

            if (bloodBank == null)
            {
                return NotFound();
            }

            return Ok(bloodBank);
        }

        // PUT: api/BloodBanks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBloodBank([FromRoute] int id, [FromBody] BloodBank bloodBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bloodBank.Id)
            {
                return BadRequest();
            }

            _context.Entry(bloodBank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BloodBankExists(id))
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

        // POST: api/BloodBanks
        [HttpPost]
        public async Task<IActionResult> PostBloodBank([FromBody] BloodBank bloodBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BloodBank.Add(bloodBank);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBloodBank", new { id = bloodBank.Id }, bloodBank);
        }

        // DELETE: api/BloodBanks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodBank([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bloodBank = await _context.BloodBank.SingleOrDefaultAsync(m => m.Id == id);
            if (bloodBank == null)
            {
                return NotFound();
            }

            _context.BloodBank.Remove(bloodBank);
            await _context.SaveChangesAsync();

            return Ok(bloodBank);
        }

        private bool BloodBankExists(int id)
        {
            return _context.BloodBank.Any(e => e.Id == id);
        }
    }
}