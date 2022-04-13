using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Data;
using Product.DTOs;
using Product.Models;

namespace Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProizvodsController : ControllerBase
    {

        private readonly IProizvodRepozitorijum _repozitorijum;
        private readonly IMapper _mapper;

        //    private readonly MockProizvodRepozitorijum repo = new MockProizvodRepozitorijum();


        public ProizvodsController(IProizvodRepozitorijum proizvodRepozitorijum, IMapper mapper)
        {

            _repozitorijum = proizvodRepozitorijum;
            _mapper = mapper;
        }

        // GET: api/Proizvods
        [HttpGet]
        public ActionResult<IEnumerable<ProizvodReadDTO>> GetProizvod()
        {
            var proizvodi = _repozitorijum.VratiProizvode();
            if (proizvodi != null)
                return Ok(_mapper.Map< IEnumerable<ProizvodReadDTO>>(proizvodi));
            return NotFound();
        }

        // GET: api/Proizvods/5
        [HttpGet("{id}", Name= "GetProizvod")]
        public ActionResult<ProizvodReadDTO> GetProizvod(long id)
        {
            var proizvod = _repozitorijum.VratiProizvodPoId(id);
            if (proizvod != null)
                return Ok(_mapper.Map<ProizvodReadDTO>(proizvod));

            return NotFound();

        }
        /*
        // PUT: api/Proizvods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProizvod(long id, Proizvod proizvod)
        {
            if (id != proizvod.Id)
            {
                return BadRequest();
            }

            //_context.Entry(proizvod).State = EntityState.Modified;

            try
            {
               // await _context.SaveChangesAsync();
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
        */

        // POST: api/Proizvods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<ProizvodReadDTO> PostProizvod(ProizvodCreateDTO proizvod)
        {
            var proizvodModel = _mapper.Map<Proizvod>(proizvod);
            _repozitorijum.KreirajProizvod(proizvodModel);
            _repozitorijum.SacuvajPromene();
      foreach(ProizvodDobavljac pd in proizvodModel.Dobavljaci)
          {

               pd.Dobavljac = new Dobavljac { Id=pd.DobavljacId, Naziv=pd.Dobavljac.Naziv};
           }
            var proizvodReadDTO = _mapper.Map<ProizvodReadDTO>(proizvodModel);
            return CreatedAtRoute(nameof(GetProizvod), new { Id = proizvodReadDTO.Id }, proizvodReadDTO); //to return also route to new product
        }
        /*
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
      */
    }
}


