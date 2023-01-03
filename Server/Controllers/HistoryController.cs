using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceDashboard.Server.Data;
using FinanceDashboard.Shared.Models;

namespace FinanceDashboard.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/History
        [HttpGet]
        public async Task<ActionResult<IEnumerable<History>>> GetHistory()
        {
            if (_context.History == null)
            {
                return NotFound();
            }

            return await _context.History.OrderByDescending(h => h.SearchTime)
                .Where(h => h.Username == User.Claims.FirstOrDefault().Value).ToListAsync();
        }

        // GET: api/History/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistory(int id)
        {
          if (_context.History == null)
          {
              return NotFound();
          }
            var history = await _context.History.FindAsync(id);

            if (history == null)
            {
                return NotFound();
            }

            return Ok(history);
        }

        // PUT: api/History/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistory(int id, History history)
        {
            if (id != history.Id)
            {
                return BadRequest();
            }

            _context.Entry(history).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoryExists(id))
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

        // POST: api/History
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<History>> PostHistory(History history)
        {
          if (_context.History == null)
          {
              return Problem("Entity set 'ApplicationDbContext.History'  is null.");
          }
            _context.History.Add(history);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistory", new { id = history.Id }, history);
        }

        // DELETE: api/History/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistory(int id)
        {
            if (_context.History == null)
            {
                return NotFound();
            }
            var history = await _context.History.FindAsync(id);
            if (history == null)
            {
                return NotFound();
            }

            _context.History.Remove(history);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistoryExists(int id)
        {
            return (_context.History?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
