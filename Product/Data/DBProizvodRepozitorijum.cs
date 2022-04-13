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
    public class DBProizvodRepozitorijum : IProizvodRepozitorijum
    {
        private readonly ProductContext _productContext;

        public DBProizvodRepozitorijum(ProductContext productContext) //dep.inj. 
        {
            _productContext = productContext;
        }

        public void Azuriraj(Proizvod proizvod)
        {
           
        }

        public void KreirajProizvod(Proizvod proizvod)
        {
            if (proizvod == null)
            {
                throw new ArgumentNullException(nameof(proizvod));
            }
            proizvod.JedinicaMere = _productContext.JedinicaMere.Find(proizvod.JedinicaMereId);
            proizvod.TipProizvoda = _productContext.TipProizvoda.Find(proizvod.TipProizvodaId);
           
            foreach (ProizvodDobavljac pd in proizvod.Dobavljaci)
            {
                pd.Dobavljac = _productContext.Dobavljac.Find(pd.DobavljacId);
                pd.Proizvod = _productContext.Proizvod.Find(pd.ProizvodId);
            }
           var product=_productContext.Proizvod.Add(proizvod);
        }

        public void ObrisiProizvod(Proizvod proizvod)
        {
            if (proizvod == null)
            {
                throw new ArgumentNullException(nameof(proizvod));
            }
            _productContext.Proizvod.Remove(proizvod);
        }

        public bool SacuvajPromene()
        {
          return (_productContext.SaveChanges()>=0);
        }

        public PagedList<Proizvod> VratiProizvode(ProizvodParameters proizvodParameters)
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
            return PagedList< Proizvod >.ToPagedList(lista, proizvodParameters.PageNumber,proizvodParameters.PageSize);
        }

        public Proizvod VratiProizvodPoId(long id)
        {
          var product= _productContext.Proizvod.Find(id);
            product.JedinicaMere = _productContext.JedinicaMere.Find(product.JedinicaMereId);
            product.TipProizvoda = _productContext.TipProizvoda.Find(product.TipProizvodaId);
            product.Dobavljaci= _productContext.ProizvodDobavljac.Where(x => x.ProizvodId == product.Id).ToList();
            foreach(ProizvodDobavljac pd in product.Dobavljaci)
            {
                pd.Dobavljac= _productContext.Dobavljac.Find(pd.DobavljacId);
                pd.Proizvod = _productContext.Proizvod.Find(pd.ProizvodId);
            }

            return product;
        }

        public PagedList<Proizvod> VratiProizvodPoKriterijumu(Expression<Func<Models.Proizvod, bool>> filter, ProizvodParameters proizvodParameters)
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
            return PagedList<Proizvod>.ToPagedList(products, proizvodParameters.PageNumber, proizvodParameters.PageSize); ;
        }

     
    }
}
