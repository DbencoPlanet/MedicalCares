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
    [Route("api/Settings")]
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Settings
        [HttpGet]
        public IEnumerable<Setting> GetSettings()
        {
            return _context.Settings;
        }

        // GET: api/Settings/5
        [HttpGet("{id}")]
        public IActionResult GetSetting([FromRoute] int id)
        {
            return Json(new { data = _context.Settings.Where(x => x.Id.Equals(id)).ToList() });
        }

        // PUT: api/Settings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSetting([FromRoute] int id, [FromBody] Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != setting.Id)
            {
                return BadRequest();
            }

            _context.Entry(setting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SettingExists(id))
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

        // POST: api/Settings
        //[HttpPost]
        //public async Task<IActionResult> PostSetting([FromBody] Setting setting)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Settings.Add(setting);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetSetting", new { id = setting.Id }, setting);
        //}


        // POST: api/Customer
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Setting customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                if (customer.Initial == null)
                {
                   
                    _context.Settings.Add(customer);

                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Add new data success." });
                }
                else
                {
                    _context.Update(customer);

                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Edit data success." });
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }

        }

        // DELETE: api/Settings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSetting([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var setting = await _context.Settings.SingleOrDefaultAsync(m => m.Id == id);
            if (setting == null)
            {
                return NotFound();
            }

            _context.Settings.Remove(setting);
            await _context.SaveChangesAsync();

            return Ok(setting);
        }

        private bool SettingExists(int id)
        {
            return _context.Settings.Any(e => e.Id == id);
        }
    }
}