﻿using System;
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
    [Route("api/Diagnoses")]
    public class DiagnosesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiagnosesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Diagnoses
        [HttpGet]
        public IEnumerable<Diagnosis> GetDiagnosis()
        {
            return _context.Diagnosis;
        }

        // GET: api/Diagnoses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiagnosis([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var diagnosis = await _context.Diagnosis.SingleOrDefaultAsync(m => m.Id == id);

            if (diagnosis == null)
            {
                return NotFound();
            }

            return Ok(diagnosis);
        }

        // PUT: api/Diagnoses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiagnosis([FromRoute] int id, [FromBody] Diagnosis diagnosis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != diagnosis.Id)
            {
                return BadRequest();
            }

            _context.Entry(diagnosis).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiagnosisExists(id))
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

        // POST: api/Diagnoses
        [HttpPost]
        public async Task<IActionResult> PostDiagnosis([FromBody] Diagnosis diagnosis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Diagnosis.Add(diagnosis);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiagnosis", new { id = diagnosis.Id }, diagnosis);
        }

        // DELETE: api/Diagnoses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnosis([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var diagnosis = await _context.Diagnosis.SingleOrDefaultAsync(m => m.Id == id);
            if (diagnosis == null)
            {
                return NotFound();
            }

            _context.Diagnosis.Remove(diagnosis);
            await _context.SaveChangesAsync();

            return Ok(diagnosis);
        }

        private bool DiagnosisExists(int id)
        {
            return _context.Diagnosis.Any(e => e.Id == id);
        }
    }
}