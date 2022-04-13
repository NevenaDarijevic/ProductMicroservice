using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
      
        // PUT: api/Proizvods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult PutProizvod(long id, ProizvodCUDTO proizvod)
        {
           
            var proizvodRepo= _repozitorijum.VratiProizvodPoId(id);
            if (proizvodRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(proizvod, proizvodRepo); //this will do update 
                                                 //but I will still use method for interface implementation, for good practice
            _repozitorijum.Azuriraj(proizvodRepo);
         //   _repozitorijum.SacuvajPromene();
            return NoContent();
        }
       

        // POST: api/Proizvods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<ProizvodReadDTO> PostProizvod(ProizvodCUDTO proizvod)
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

        // PATCH: api/Proizvods/5
        [HttpPatch("{id}")]
        public ActionResult PatchProizvod(long id, JsonPatchDocument<ProizvodCUDTO> patchDocument) //partional update
        {

            var proizvodRepo = _repozitorijum.VratiProizvodPoId(id);
            if (proizvodRepo == null)
            {
                return NotFound();
            }
            var proizvodPatch = _mapper.Map<ProizvodCUDTO>(proizvodRepo);
            patchDocument.ApplyTo(proizvodPatch, ModelState);
            if (!TryValidateModel(proizvodPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(proizvodPatch, proizvodRepo);
            _repozitorijum.Azuriraj(proizvodRepo);
           _repozitorijum.SacuvajPromene();
            return NoContent();
        }
    }
}


