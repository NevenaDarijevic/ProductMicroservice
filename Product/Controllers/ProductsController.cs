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
            if(string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                var productsAll = _repository.GetAllProducts(productParameters);
                if (productsAll == null)
                {
                    _logger.Information("Korisnik je zeleo da pretrazi proizvode prema nazivu, ali nije uneo kriterijum usled cega bi se prikazali svi proizvodi. Medjutim, nema vise proizvoda u bazi.");
                    return NotFound();
                }
                else
                {
                    _logger.Information("Korisniku su se prikazali podaci o svim proizvodima s obzirom da nije uneo naziv radi pretrage.");
                    return Ok(_mapper.Map<IEnumerable<ProizvodReadDTO>>(productsAll));
                }
            }
            var products = _repository.GetByCriteria(x=>x.Naziv==name, productParameters);
            if (products != null && products.Count!=0)
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
            if (price<=0)
            {
                var productsAll = _repository.GetAllProducts(productParameters);
                if (productsAll == null)
                {
                    _logger.Information("Korisnik je zeleo da pretrazi proizvode prema ceni, ali nije uneo kriterijum usled cega bi se prikazali svi proizvodi. Medjutim, nema vise proizvoda u bazi.");
                    return NotFound();
                }
                else
                {
                    _logger.Information("Korisniku su se prikazali podaci o svim proizvodima s obzirom da nije uneo cenu radi pretrage.");
                    return Ok(_mapper.Map<IEnumerable<ProizvodReadDTO>>(productsAll));
                }
            }
            var products = _repository.GetByCriteria(x => x.Cena==price, productParameters);
            if (products != null && products.Count != 0)
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
            if (pdv <= 0)
            {
                var productsAll = _repository.GetAllProducts(productParameters);
                if (productsAll == null)
                {
                    _logger.Information("Korisnik je zeleo da pretrazi proizvode prema PDV-u, ali nije uneo kriterijum usled cega bi se prikazali svi proizvodi. Medjutim, nema vise proizvoda u bazi.");
                    return NotFound();
                }
                else
                {
                    _logger.Information("Korisniku su se prikazali podaci o svim proizvodima s obzirom da nije uneo PDV radi pretrage.");
                    return Ok(_mapper.Map<IEnumerable<ProizvodReadDTO>>(productsAll));
                }
            }
            var products = _repository.GetByCriteria(x => x.Pdv == pdv, productParameters);
            if (products != null && products.Count != 0)
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
            if (measurementUnit<=0)
            {
                var productsAll = _repository.GetAllProducts(productParameters);
                if (productsAll == null)
                {
                    _logger.Information("Korisnik je zeleo da pretrazi proizvode prema jedinici mere, ali nije uneo kriterijum usled cega bi se prikazali svi proizvodi. Medjutim, nema vise proizvoda u bazi.");
                    return NotFound();
                }
                else
                {
                    _logger.Information("Korisniku su se prikazali podaci o svim proizvodima s obzirom da nije uneo jedinicu mere radi pretrage.");
                    return Ok(_mapper.Map<IEnumerable<ProizvodReadDTO>>(productsAll));
                }
            }
            var products = _repository.GetByCriteria(x => x.JedinicaMere.Id== measurementUnit, productParameters);
            if (products != null && products.Count != 0)
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
            if (type <= 0)
            {
                var productsAll = _repository.GetAllProducts(productParameters);
                if (productsAll == null)
                {
                    _logger.Information("Korisnik je zeleo da pretrazi proizvode prema tipu, ali nije uneo kriterijum usled cega bi se prikazali svi proizvodi. Medjutim, nema vise proizvoda u bazi.");
                    return NotFound();
                }
                else
                {
                    _logger.Information("Korisniku su se prikazali podaci o svim proizvodima s obzirom da nije uneo tip radi pretrage.");
                    return Ok(_mapper.Map<IEnumerable<ProizvodReadDTO>>(productsAll));
                }
            }
            var products = _repository.GetByCriteria(x => x.TipProizvoda.Id== type, productParameters);
            if (products != null && products.Count != 0)
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
             var  productRepo = _repository.GetProductById(id);
            if (productRepo == null)
            {
                _logger.Information("Korisniku se prikazuje poruka o neuspesnosti azuriranja jer proizvod nije pronadjen.");
                return NotFound();
            }
            _mapper.Map(product, productRepo);
            try
            {
                _repository.Update(productRepo);
            }
            catch (Exception e)
            {
                _logger.Information("Korisniku se prikazuje poruka o neuspesnosti azuriranja proizvoda zbog neispravno unetih podataka.");
                return BadRequest(e);
            }
            _repository.SaveChanges();
            _logger.Information("Korisnik je uspesno azurirao proizvod.");
            return Ok();
        }
       

       
        [HttpPost]
        public ActionResult<ProizvodReadDTO> PostProduct(ProizvodCreateAndUpdateDTO product)
        {
            var productModel = _mapper.Map<Proizvod>(product);
            try
            {
                _repository.Create(productModel);
            }
            catch(Exception e)
            {
                _logger.Information("Korisniku se prikazuje poruka o neuspesnosti dodavanja novog proizvoda zbog neispravno unetih podataka.");
                return BadRequest(e);
            }
        
            _repository.SaveChanges();
              foreach(ProizvodDobavljac pd in productModel.Dobavljaci)
              {
                   pd.Dobavljac = new Dobavljac { Id=pd.DobavljacId, Naziv=pd.Dobavljac.Naziv};
              }
            var productReadDTO = _mapper.Map<ProizvodReadDTO>(productModel);
            _logger.Information("Korisnik je uspesno dodao proizvod.");
            return CreatedAtRoute(nameof(GetProductById), new { Id = productReadDTO.Id }, productReadDTO); //to return also route to new product
        }

      
    
    }
}


