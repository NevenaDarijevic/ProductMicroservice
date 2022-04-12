using Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void KreirajProizvod(Proizvod proizvod)
        {
            if (proizvod == null)
            {
                throw new ArgumentNullException(nameof(proizvod));
            }
            _productContext.Proizvod.Add(proizvod);
        }

        public bool SacuvajPromene()
        {
          return (_productContext.SaveChanges()>=0);
        }

        public IEnumerable<Proizvod> VratiProizvode()
        {
           var lista= _productContext.Proizvod.ToList();
            foreach(var product in lista)
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
            return lista;
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
    }
}
