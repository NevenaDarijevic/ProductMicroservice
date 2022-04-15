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

        public void Update(Proizvod proizvod)
        {
           
        }

        public void Create(Proizvod proizvod)
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

        public bool SaveChanges()
        {
          return (_productContext.SaveChanges()>=0);
        }

        public PagedList<Proizvod> VratiProizvode(ProductParameters productParameters)
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
