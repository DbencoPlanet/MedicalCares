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
    [Route("api/NoticeBoards")]
    public class NoticeBoardsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoticeBoardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/NoticeBoards
        [HttpGet]
        public IEnumerable<NoticeBoard> GetNoticeBoard()
        {
            return _context.NoticeBoard;
        }

        // GET: api/NoticeBoards/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoticeBoard([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var noticeBoard = await _context.NoticeBoard.SingleOrDefaultAsync(m => m.Id == id);

            if (noticeBoard == null)
            {
                return NotFound();
            }

            return Ok(noticeBoard);
        }

        // PUT: api/NoticeBoards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNoticeBoard([FromRoute] int id, [FromBody] NoticeBoard noticeBoard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != noticeBoard.Id)
            {
                return BadRequest();
            }

            _context.Entry(noticeBoard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticeBoardExists(id))
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

        // POST: api/NoticeBoards
        [HttpPost]
        public async Task<IActionResult> PostNoticeBoard([FromBody] NoticeBoard noticeBoard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.NoticeBoard.Add(noticeBoard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNoticeBoard", new { id = noticeBoard.Id }, noticeBoard);
        }

        // DELETE: api/NoticeBoards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoticeBoard([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var noticeBoard = await _context.NoticeBoard.SingleOrDefaultAsync(m => m.Id == id);
            if (noticeBoard == null)
            {
                return NotFound();
            }

            _context.NoticeBoard.Remove(noticeBoard);
            await _context.SaveChangesAsync();

            return Ok(noticeBoard);
        }

        private bool NoticeBoardExists(int id)
        {
            return _context.NoticeBoard.Any(e => e.Id == id);
        }
    }
}