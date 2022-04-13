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
using Product.Models.Parameters;

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

       
        [HttpGet]
        public ActionResult<IEnumerable<ProizvodReadDTO>> GetProizvod([FromQuery] ProizvodParameters proizvodParameters)
        {
            var proizvodi = _repozitorijum.VratiProizvode(proizvodParameters);
            if (proizvodi != null)
                return Ok(_mapper.Map< IEnumerable<ProizvodReadDTO>>(proizvodi));
            return NotFound();
        }

       
        [HttpGet("getbyid/{id}", Name= "GetProizvod")]
        public ActionResult<ProizvodReadDTO> GetProizvod(long id)
        {
            var proizvod = _repozitorijum.VratiProizvodPoId(id);
            if (proizvod != null)
                return Ok(_mapper.Map<ProizvodReadDTO>(proizvod));

            return NotFound();

        }

      
        [HttpGet("getbynaziv/{naziv}")]
        public ActionResult<ProizvodReadDTO> VratiPoNazivu(string naziv, [FromQuery] ProizvodParameters proizvodParameters)
        {
            var proizvodi = _repozitorijum.VratiProizvodPoKriterijumu(x=>x.Naziv==naziv, proizvodParameters);
            if (proizvodi != null)
                return Ok(_mapper.Map<IEnumerable<ProizvodReadDTO>>(proizvodi));

            return NotFound();

        }

       
        [HttpGet("getbycena/{cena:double}")]
        public ActionResult<ProizvodReadDTO> VratiPoCeni(double cena, [FromQuery] ProizvodParameters proizvodParameters)
        {
            var proizvodi = _repozitorijum.VratiProizvodPoKriterijumu(x => x.Cena==cena, proizvodParameters);
            if (proizvodi != null)
                return Ok(_mapper.Map<IEnumerable<ProizvodReadDTO>>(proizvodi));

            return NotFound();

        }

       
        [HttpGet("getbypdv/{pdv:double}")]
        public ActionResult<ProizvodReadDTO> VratiPoPdv(double pdv, [FromQuery] ProizvodParameters proizvodParameters)
        {
            var proizvodi = _repozitorijum.VratiProizvodPoKriterijumu(x => x.Pdv == pdv, proizvodParameters);
            if (proizvodi != null)
                return Ok(_mapper.Map<IEnumerable<ProizvodReadDTO>>(proizvodi));

            return NotFound();

        }

        [HttpGet("getbyjedinicamere/{jedinicamere:long}")]
        public ActionResult<ProizvodReadDTO> VratiPoJediniciMere(long jedinicamere, [FromQuery] ProizvodParameters proizvodParameters)
        {
            var proizvodi = _repozitorijum.VratiProizvodPoKriterijumu(x => x.JedinicaMere.Id== jedinicamere, proizvodParameters);
            if (proizvodi != null)
                return Ok(_mapper.Map<IEnumerable<ProizvodReadDTO>>(proizvodi));

            return NotFound();

        }

        [HttpGet("getbytip/{tip}")]
        public ActionResult<ProizvodReadDTO> VratiPoTipuProizvoda(long tip, [FromQuery] ProizvodParameters proizvodParameters)
        {
            var proizvodi = _repozitorijum.VratiProizvodPoKriterijumu(x => x.TipProizvoda.Id==tip, proizvodParameters);
            if (proizvodi != null)
                return Ok(_mapper.Map<IEnumerable<ProizvodReadDTO>>(proizvodi));

            return NotFound();

        }

       

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

       
        [HttpDelete("{id}")]
        public ActionResult DeleteProizvod(long id)
        {
         var proizvod = _repozitorijum.VratiProizvodPoId(id);
         if (proizvod == null)
         {
             return NotFound();
         }

            _repozitorijum.ObrisiProizvod(proizvod);
            _repozitorijum.SacuvajPromene();

            return NoContent();
        }


       
    }
}


