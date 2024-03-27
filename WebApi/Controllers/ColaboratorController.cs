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
    public class ColaboratorController(DataContext _context) : ControllerBase
    {
        // GET: api/Colaborator
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Colaborator>>> GetColaborators()
        {
            return await _context.Colaborators.ToListAsync();
        }

        // GET: api/Colaborator/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Colaborator>> GetColaborator(long id)
        {
            var colaborator = await _context.Colaborators.FindAsync(id);

            if (colaborator == null)
            {
                return NotFound();
            }

            return colaborator;
        }

        // PUT: api/Colaborator/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColaborator(long id, Colaborator colaborator)
        {
            if (id != colaborator.Id)
            {
                return BadRequest();
            }

            _context.Entry(colaborator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColaboratorExists(id))
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

        // POST: api/Colaborator
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Colaborator>> PostColaborator(Colaborator colaborator)
        {
            _context.Colaborators.Add(colaborator);
            await _context.SaveChangesAsync();
    // return Ok();
            return CreatedAtAction(nameof(PostColaborator), new { id = colaborator.Id }, colaborator);
            // return CreatedAtAction(nameof(Colaborator), new { id = colaborator.Id }, colaborator);
        }

        // DELETE: api/Colaborator/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColaborator(long id)
        {
            var colaborator = await _context.Colaborators.FindAsync(id);
            if (colaborator == null)
            {
                return NotFound();
            }

            _context.Colaborators.Remove(colaborator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColaboratorExists(long id)
        {
            return _context.Colaborators.Any(e => e.Id == id);
        }
    }
}
