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
    public class HolidayPeriodController(DataContext _context) : ControllerBase
    {
        // GET: api/HolidayPeriod
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HolidayPeriod>>> GetHolidayPeriod()
        {
            return await _context.HolidayPeriod.ToListAsync();
        }

        // GET: api/HolidayPeriod/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HolidayPeriod>> GetHolidayPeriod(long id)
        {
            var holidayPeriod = await _context.HolidayPeriod.FindAsync(id);

            if (holidayPeriod == null)
            {
                return NotFound();
            }

            return holidayPeriod;
        }

        // PUT: api/HolidayPeriod/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHolidayPeriod(long id, HolidayPeriod holidayPeriod)
        {
            if (id != holidayPeriod.Id)
            {
                return BadRequest();
            }

            _context.Entry(holidayPeriod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HolidayPeriodExists(id))
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

        // POST: api/HolidayPeriod
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HolidayPeriod>> PostHolidayPeriod(HolidayPeriod holidayPeriod)
        {
            _context.HolidayPeriod.Add(holidayPeriod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHolidayPeriod", new { id = holidayPeriod.Id }, holidayPeriod);
        }

        // DELETE: api/HolidayPeriod/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHolidayPeriod(long id)
        {
            var holidayPeriod = await _context.HolidayPeriod.FindAsync(id);
            if (holidayPeriod == null)
            {
                return NotFound();
            }

            _context.HolidayPeriod.Remove(holidayPeriod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HolidayPeriodExists(long id)
        {
            return _context.HolidayPeriod.Any(e => e.Id == id);
        }
    }
}
