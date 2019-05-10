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
    [Route("api/InvoiceLines")]
    public class InvoiceLinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceLinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/InvoiceLines
        [HttpGet]
        public IEnumerable<InvoiceLine> GetInvoiceLines()
        {
            return _context.InvoiceLines;
        }

        // GET: api/InvoiceLines/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvoiceLine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceLine = await _context.InvoiceLines.SingleOrDefaultAsync(m => m.Id == id);

            if (invoiceLine == null)
            {
                return NotFound();
            }

            return Ok(invoiceLine);
        }

        // PUT: api/InvoiceLines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceLine([FromRoute] int id, [FromBody] InvoiceLine invoiceLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoiceLine.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoiceLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceLineExists(id))
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

        // POST: api/InvoiceLines
        [HttpPost]
        public async Task<IActionResult> PostInvoiceLine([FromBody] InvoiceLine invoiceLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (invoiceLine.InvoiceId == null)
            {
                invoiceLine.SubTotal = (decimal)invoiceLine.Quantity * invoiceLine.Price;
                _context.InvoiceLines.Add(invoiceLine);



                Invoice vi = await _context.Invoices.Include(x => x.InvoiceLine).SingleOrDefaultAsync(x => x.Id.Equals(invoiceLine.InvoiceId));
                vi.Total = vi.InvoiceLine.Sum(x => x.SubTotal);

                vi.Vat = vi.Vat / 100 * vi.Total;
                vi.Discount = vi.Discount / 100 * vi.Total;
                vi.GrandTotal = vi.Total + vi.Vat - vi.Discount;
                vi.Due = vi.GrandTotal - vi.Paid;
                _context.Invoices.Update(vi);

                _context.InvoiceLines.Add(invoiceLine);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Add new data success." });

            }
            else
            {
               
                if (invoiceLine.InvoiceId != invoiceLine.Id)
                {
                    invoiceLine.SubTotal = (decimal)invoiceLine.Quantity * invoiceLine.Price;
                    _context.Update(invoiceLine);



                    Invoice vi = await _context.Invoices.Include(x => x.InvoiceLine).SingleOrDefaultAsync(x => x.Id.Equals(invoiceLine.InvoiceId));
                    vi.Total = vi.InvoiceLine.Sum(x => x.SubTotal);

                    vi.Vat = vi.Vat / 100 * vi.Total;
                    vi.Discount = vi.Discount / 100 * vi.Total;
                    vi.GrandTotal = vi.Total + vi.Vat - vi.Discount;
                    vi.Due = vi.GrandTotal - vi.Paid;
                    _context.Invoices.Update(vi);



                    _context.Update(invoiceLine);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "Edit data success." });

                  

                }

            }
            return Ok(invoiceLine);
            //return CreatedAtAction("GetInvoiceLine", new { id = invoiceLine.Id }, invoiceLine);
        }

        // DELETE: api/InvoiceLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoiceLine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var invoiceLine = await _context.InvoiceLines.SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceLine == null)
            {
                return NotFound();
            }

            _context.InvoiceLines.Remove(invoiceLine);
            await _context.SaveChangesAsync();

            return Ok(invoiceLine);
        }

        private bool InvoiceLineExists(int id)
        {
            return _context.InvoiceLines.Any(e => e.Id == id);
        }
    }
}