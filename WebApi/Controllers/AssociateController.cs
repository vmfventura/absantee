using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssociateController : ControllerBase
    {
        private readonly DataContext _context;

        public AssociateController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Associate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Associate>>> GetAssociate()
        {
            return await _context.Associate.ToListAsync();
        }

        // GET: api/Associate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Associate>> GetAssociate(long id)
        {
            var associate = await _context.Associate.FindAsync(id);

            if (associate == null)
            {
                return NotFound();
            }

            return associate;
        }

        // PUT: api/Associate/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssociate(long id, Associate associate)
        {
            if (id != associate.Id)
            {
                return BadRequest();
            }

            _context.Entry(associate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssociateExists(id))
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

        // POST: api/Associate
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Associate>> PostAssociate(Associate associate)
        {
            _context.Associate.Add(associate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssociate", new { id = associate.Id }, associate);
        }

        // DELETE: api/Associate/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssociate(long id)
        {
            var associate = await _context.Associate.FindAsync(id);
            if (associate == null)
            {
                return NotFound();
            }

            _context.Associate.Remove(associate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AssociateExists(long id)
        {
            return _context.Associate.Any(e => e.Id == id);
        }
    }
}
