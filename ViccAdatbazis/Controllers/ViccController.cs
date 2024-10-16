using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ViccAdatbazis.Data;
using ViccAdatbazis.Models;

namespace ViccAdatbazis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViccController : ControllerBase
    {
        private readonly ViccDbContext _context;
        public ViccController(ViccDbContext context)
        {
            _context = context;
        }
        [HttpGet]

        public async Task<ActionResult<List<Vicc>>> GetViccek()
        {
            return await _context.Viccek.Where(x => x.Aktiv == true).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vicc>> GetVicc(int id)
        {
            var vicc = await _context.Viccek.FindAsync(id);
            return vicc == null ? NotFound() : vicc;
        }


        [HttpPost]
        public async Task<ActionResult<Vicc>> PostVicc(Vicc vicc)
        {
            _context.Viccek.Add(vicc);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetVicc", new { id = vicc.Id }, vicc);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> PutVicc(int id, Vicc vicc)
        {
            if (id != vicc.Id)
            {
                return BadRequest();
            }
            _context.Entry(vicc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVicc(int id)
        {
            var vicc = await _context.Viccek.FindAsync(id);
            if (vicc == null)
            {
                return BadRequest();
            }
            if (vicc.Aktiv == true)
            {
                vicc.Aktiv = false;
            }
            else
            {
                _context.Viccek.Remove(vicc);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [Route("like/{id}")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<string>> Tetszik(int id) 
        {
            var vicc = await _context.Viccek.FindAsync(id);
            if (vicc == null)
            {
                return NotFound();
            }
            vicc.Tetszik++;
            _context.Entry(vicc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var valasz = new
            {
                tdb = vicc.Tetszik
            };
            return Ok(JsonSerializer.Serialize(valasz));
        }
        [Route("dislike/{id}")]
        [HttpPatch("{id}")]
        public async Task<ActionResult> NemTetszik(int id)
        {
            var vicc = await _context.Viccek.FindAsync(id);
            if (vicc == null)
            {
                return NotFound();
            }
            vicc.NemTetszik++;
            _context.Entry(vicc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var valasz = new
            {
                tdb = vicc.NemTetszik
            };
            return Ok(JsonSerializer.Serialize(valasz));
        }
    }
}