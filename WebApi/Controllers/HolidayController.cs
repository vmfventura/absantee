using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
// using WebApi.Context;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController(DataContext _context) : ControllerBase
    {
        // GET: api/Holiday
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Holiday>>> GetHolidayList()
        {
            return await _context.Holiday.ToListAsync();
        }

        // GET: api/Holiday/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Holiday>> GetHoliday(long id)
        {
            var holiday = await _context.Holiday.FindAsync(id);

            if (holiday == null)
            {
                return NotFound();
            }

            return holiday;
        }

        // PUT: api/Holiday/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHoliday(long id, Holiday holiday)
        {
            if (id != holiday.Id)
            {
                return BadRequest();
            }

            _context.Entry(holiday).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HolidayExists(id))
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

        // POST: api/Holiday
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Holiday>> PostHoliday(Holiday holiday)
        {
            _context.Holiday.Add(holiday);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHoliday", new { id = holiday.Id }, holiday);
        }

        // DELETE: api/Holiday/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoliday(long id)
        {
            var holiday = await _context.Holiday.FindAsync(id);
            if (holiday == null)
            {
                return NotFound();
            }

            _context.Holiday.Remove(holiday);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HolidayExists(long id)
        {
            return _context.Holiday.Any(e => e.Id == id);
        }
    }
}
