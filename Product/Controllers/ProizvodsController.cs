using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Data;
using Product.Models;

namespace Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProizvodsController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly IProizvodRepozitorijum _repozitorijum;

        //    private readonly MockProizvodRepozitorijum repo = new MockProizvodRepozitorijum();

        //Repository pattern
        public ProizvodsController(ProductContext context,IProizvodRepozitorijum proizvodRepozitorijum)
        {
            _context = context;
            _repozitorijum = proizvodRepozitorijum;
        }

        // GET: api/Proizvods
        [HttpGet]
        public ActionResult<IEnumerable<Proizvod>> GetProizvod()
        {
            var proizvodi = _repozitorijum.VratiProizvode();
            return Ok(proizvodi);
        }

        // GET: api/Proizvods/5
        [HttpGet("{id}")]
        public ActionResult<Proizvod> GetProizvod(long id)
        {
            var proizvod = _repozitorijum.VratiProizvodPoId(id);
            return Ok(proizvod);

        }

        // PUT: api/Proizvods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProizvod(long id, Proizvod proizvod)
        {
            if (id != proizvod.Id)
            {
                return BadRequest();
            }

            _context.Entry(proizvod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProizvodExists(id))
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

        // POST: api/Proizvods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proizvod>> PostProizvod(Proizvod proizvod)
        {
            _context.Proizvod.Add(proizvod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProizvod", new { id = proizvod.Id }, proizvod);
        }

        // DELETE: api/Proizvods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProizvod(long id)
        {
            var proizvod = await _context.Proizvod.FindAsync(id);
            if (proizvod == null)
            {
                return NotFound();
            }

            _context.Proizvod.Remove(proizvod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProizvodExists(long id)
        {
            return _context.Proizvod.Any(e => e.Id == id);
        }
    }
}
