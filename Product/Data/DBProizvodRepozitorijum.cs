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
            return _productContext.Proizvod.ToList();
        }

        public Proizvod VratiProizvodPoId(long id)
        {
            return _productContext.Proizvod.FirstOrDefault(x => x.Id == id);
        }
    }
}
