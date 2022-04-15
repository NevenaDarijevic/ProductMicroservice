using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Product.Data;
using Product.DTOs;
using Product.Logging;
using Product.Models;
using Product.Models.Parameters;

namespace Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILog _logger;


        public ProductsController(IProductRepository productRepository, IMapper mapper, ILog logger)
        {
            _repository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

 
        [HttpGet("getById/{id}", Name= "GetProductById")]
        public ActionResult<ProizvodReadDTO> GetProductById(long id)
        {
            var product = _repository.GetProductById(id);
            if (product != null)
            {
                _logger.Information("Korisniku su se prikazali podaci o izabranom proizvodu.");
                return Ok(_mapper.Map<ProizvodReadDTO>(product));
            }
            else
            {
                _logger.Information("Korisniku se nije prikazao zeljeni proizvod jer nije pronadjen.");
                return NotFound();
            }
        }

      
        [HttpGet("getByName/{name}")]
        public ActionResult<ProizvodReadDTO> GetByName(string name, [FromQuery] ProductParameters productParameters)
        {
            var products = _repository.GetByCriteria(x=>x.Naziv==name, productParameters);
            if (products != null)
            {
                _logger.Information("Korisniku su se prikazali podaci o proizvodima prema unetom nazivu.");
                return Ok(_mapper.Map<IEnumerable<ProizvodReadDTO>>(products));
            }
            else
            {
                _logger.Information("Korisniku se nisu prikazali podaci o proizvodima prema unetom nazivu jer nisu pronadjeni.");
                return NotFound();
            }
        }

       
        [HttpGet("getByPrice/{price:double}")]
        public ActionResult<ProizvodReadDTO> GetByPrice(double price, [FromQuery] ProductParameters productParameters)
        {
            var products = _repository.GetByCriteria(x => x.Cena==price, productParameters);
            if (products != null)
            {
                _logger.Information("Korisniku su se prikazali podaci o proizvodima prema unetoj ceni.");
                return Ok(_mapper.Map<IEnumerable<ProizvodReadDTO>>(products));
            }
            else
            {
                _logger.Information("Korisniku se nisu prikazali podaci o proizvodima prema unetoj ceni jer nisu pronadjeni.");
                return NotFound();
            }

        }

       
        [HttpGet("getByPDV/{pdv:double}")]
        public ActionResult<ProizvodReadDTO> GetByPDV(double pdv, [FromQuery] ProductParameters productParameters)
        {
            var products = _repository.GetByCriteria(x => x.Pdv == pdv, productParameters);
            if (products != null)
            {
                _logger.Information("Korisniku su se prikazali podaci o proizvodima prema unetom PDV-u.");
                return Ok(_mapper.Map<IEnumerable<ProizvodReadDTO>>(products));
            }
            else
            {
                _logger.Information("Korisniku se nisu prikazali podaci o proizvodima prema unetom PDV-u jer nisu pronadjeni.");
                return NotFound();
            }
        }

        [HttpGet("getByMeasurementUnit/{measurementUnit:long}")]
        public ActionResult<ProizvodReadDTO> GetByMeasurementUnit(long measurementUnit, [FromQuery] ProductParameters productParameters)
        {
            var products = _repository.GetByCriteria(x => x.JedinicaMere.Id== measurementUnit, productParameters);
            if (products != null)
            {
                _logger.Information("Korisniku su se prikazali podaci o proizvodima prema unetoj jedinici mere proizvoda.");
                return Ok(_mapper.Map<IEnumerable<ProizvodReadDTO>>(products));
            }
            else
            {
                _logger.Information("Korisniku se nisu prikazali podaci o proizvodima prema unetoj jedinici mere proizvoda jer nisu pronadjeni.");
                return NotFound();
            }
        }

        [HttpGet("getByType/{type}")]
        public ActionResult<ProizvodReadDTO> GetByType(long type, [FromQuery] ProductParameters productParameters)
        {
            var products = _repository.GetByCriteria(x => x.TipProizvoda.Id== type, productParameters);
            if (products != null)
            {
                _logger.Information("Korisniku su se prikazali podaci o proizvodima prema unetom tipu proizvoda.");
                return Ok(_mapper.Map<IEnumerable<ProizvodReadDTO>>(products));
            }
            else
            {
                _logger.Information("Korisniku se nisu prikazali podaci o proizvodima prema unetom tipu proizvoda jer nisu pronadjeni.");
                return NotFound();
            }

        }

       
        [HttpPut("{id}")]
        public ActionResult PutProizvod(long id, ProizvodCreateAndUpdateDTO product)
        {
           
            var productRepo = _repository.GetProductById(id);
            if (productRepo == null)
            {
                _logger.Information("Korisniku se prikazuje poruka o neuspesnosti azuriranja jer proizvod nije pronadjen.");
                return NotFound();
            }
            _mapper.Map(product, productRepo);  
            _repository.Update(productRepo);
            //   _repository.SacuvajPromene();
            _logger.Information("Korisnik je uspesno azurirao proizvod.");
            return NoContent();
        }
       

       
        [HttpPost]
        public ActionResult<ProizvodReadDTO> PostProduct(ProizvodCreateAndUpdateDTO product)
        {
            var productModel = _mapper.Map<Proizvod>(product);
            _repository.Create(productModel);
            _repository.SaveChanges();
              foreach(ProizvodDobavljac pd in productModel.Dobavljaci)
              {
                   pd.Dobavljac = new Dobavljac { Id=pd.DobavljacId, Naziv=pd.Dobavljac.Naziv};
              }
            var productReadDTO = _mapper.Map<ProizvodReadDTO>(productModel);
            _logger.Information("Korisnik je uspesno dodao proizvod.");
            return CreatedAtRoute(nameof(GetProductById), new { Id = productReadDTO.Id }, productReadDTO); //to return also route to new product
        }

      
        [HttpPatch("{id}")]
        public ActionResult PatchProduct(long id, JsonPatchDocument<ProizvodCreateAndUpdateDTO> patchDocument) //partional update
        {
            var productRepo = _repository.GetProductById(id);
            if (productRepo == null)
            {
                _logger.Information("Korisniku se prikazuje poruka o neuspesnosti azuriranja jer proizvod nije pronadjen.");
                return NotFound();
            }
            var productPatch = _mapper.Map<ProizvodCreateAndUpdateDTO>(productRepo);
            patchDocument.ApplyTo(productPatch, ModelState);
            if (!TryValidateModel(productPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(productPatch, productRepo);
            _repository.Update(productRepo);
            _repository.SaveChanges();
            _logger.Information("Korisnik je uspesno azurirao proizvod.");
            return NoContent();
        }
    }
}


