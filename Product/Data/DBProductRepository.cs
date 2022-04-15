using Microsoft.EntityFrameworkCore;
using Product.Models;
using Product.Models.Helpers;
using Product.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Product.Data
{
    public class DBProductRepository : IProductRepository
    {
        private readonly ProductContext _productContext;

        public DBProductRepository(ProductContext productContext) //dep.inj. 
        {
            _productContext = productContext;
        }

        public void Update(Proizvod product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            var productToUpdate = _productContext.Proizvod.Find(product.Id);
            productToUpdate.Naziv = product.Naziv;
            productToUpdate.Cena = product.Cena;
            productToUpdate.TipProizvodaId = product.TipProizvodaId;
            productToUpdate.JedinicaMereId = product.JedinicaMereId;

            productToUpdate.JedinicaMere = _productContext.JedinicaMere.Find(product.JedinicaMereId);
            productToUpdate.TipProizvoda = _productContext.TipProizvoda.Find(product.TipProizvodaId);
            if (product.JedinicaMere == null)
            {
                throw new Exception("Ne postoji jedinica mere sa ovim ID-em.");
            }
            if (product.TipProizvoda == null)
            {
                throw new Exception("Ne postoji tip proizvoda sa ovim ID-em.");
            }
                if (string.IsNullOrEmpty(product.Naziv))
            {
                throw new Exception("Naziv ne sme biti prazan string.");
            }
            if (product.Pdv < 0)
            {
                throw new Exception("PDV ne moze biti u minusu.");
            }
            if (product.Cena <= 0)
            {
                throw new Exception("Cena mora biti veca od nule.");
            }
            foreach (ProizvodDobavljac pd in productToUpdate.Dobavljaci)
            {
                pd.Dobavljac = _productContext.Dobavljac.Find(pd.DobavljacId);
                if (pd.Dobavljac == null)
                {
                    throw new Exception("Ne postoji dobavljac sa unetim ID-em.");
                }
                pd.Proizvod = _productContext.Proizvod.Find(pd.ProizvodId);
            }
    }

        public void Create(Proizvod product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            product.JedinicaMere = _productContext.JedinicaMere.Find(product.JedinicaMereId);
            product.TipProizvoda = _productContext.TipProizvoda.Find(product.TipProizvodaId);
            if (product.JedinicaMere == null)
            {
                throw new Exception("Ne postoji jedinica mere sa ovim ID-em.");
            }
            if (product.TipProizvoda == null)
            {
                throw new Exception("Ne postoji tip proizvoda sa ovim ID-em.");
            }
            if (string.IsNullOrEmpty(product.Naziv))
            {
                throw new Exception("Naziv ne sme biti prazan string.");
            }
            if (product.Pdv < 0)
            {
                throw new Exception("PDV ne moze biti u minusu.");
            }
            if (product.Cena <= 0)
            {
                throw new Exception("Cena mora biti veca od nule.");
            }
            foreach (ProizvodDobavljac pd in product.Dobavljaci)
            {
                pd.Dobavljac = _productContext.Dobavljac.Find(pd.DobavljacId);
                if (pd.Dobavljac == null)
                {
                    throw new Exception("Ne postoji dobavljac sa unetim ID-em.");
                }
                pd.Proizvod = _productContext.Proizvod.Find(pd.ProizvodId);
            }
           var productNew=_productContext.Proizvod.Add(product);
        }

      

        public bool SaveChanges()
        {
          return (_productContext.SaveChanges()>=0);
        }

        public PagedList<Proizvod> GetAllProducts(ProductParameters productParameters)
        {
            IEnumerable<Proizvod> lista = _productContext.Proizvod.ToList();
           

            foreach (var product in lista)
            {
                
                product.JedinicaMere = _productContext.JedinicaMere.Find(product.JedinicaMereId);
                product.TipProizvoda = _productContext.TipProizvoda.Find(product.TipProizvodaId);
                product.Dobavljaci = _productContext.ProizvodDobavljac.Where(x => x.ProizvodId == product.Id).ToList();
                foreach (ProizvodDobavljac pd in product.Dobavljaci)
                {
                    pd.Dobavljac = _productContext.Dobavljac.Find(pd.DobavljacId);
                    pd.Proizvod = _productContext.Proizvod.Find(pd.ProizvodId);
                }

            }
            return PagedList< Proizvod >.ToPagedList(lista, productParameters.PageNumber,productParameters.PageSize);
        }

        public Proizvod GetProductById(long id)
        {
           
            var product= _productContext.Proizvod.Find(id);
            if (product != null)
            {
                product.JedinicaMere = _productContext.JedinicaMere.Find(product.JedinicaMereId);
                product.TipProizvoda = _productContext.TipProizvoda.Find(product.TipProizvodaId);
                product.Dobavljaci = _productContext.ProizvodDobavljac.Where(x => x.ProizvodId == product.Id).ToList();
                foreach (ProizvodDobavljac pd in product.Dobavljaci)
                {
                    pd.Dobavljac = _productContext.Dobavljac.Find(pd.DobavljacId);
                    pd.Proizvod = _productContext.Proizvod.Find(pd.ProizvodId);
                }

                return product;
            }
            else
            {
                return null;
            }
        }

        public PagedList<Proizvod> GetByCriteria(Expression<Func<Models.Proizvod, bool>> filter, ProductParameters productParameters)
        {
            IEnumerable<Proizvod> products = _productContext.Proizvod.Include(t => t.JedinicaMere).Include(t => t.TipProizvoda).Where(filter);
            foreach (Proizvod product in products)
            {
                product.JedinicaMere = _productContext.JedinicaMere.Find(product.JedinicaMereId);
                product.TipProizvoda = _productContext.TipProizvoda.Find(product.TipProizvodaId);
                product.Dobavljaci = _productContext.ProizvodDobavljac.Where(x => x.ProizvodId == product.Id).ToList();
                foreach (ProizvodDobavljac pd in product.Dobavljaci)
                {
                    pd.Dobavljac = _productContext.Dobavljac.Find(pd.DobavljacId);
                    pd.Proizvod = _productContext.Proizvod.Find(pd.ProizvodId);
                }
            }
            return PagedList<Proizvod>.ToPagedList(products, productParameters.PageNumber, productParameters.PageSize); ;
        }

     
    }
}
