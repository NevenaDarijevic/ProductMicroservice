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
     public interface IProductRepository
    {
        PagedList<Proizvod> VratiProizvode(ProductParameters proizvodParameters);
        Proizvod VratiProizvodPoId(long id);
        void KreirajProizvod(Proizvod proizvod);
        bool SacuvajPromene();
        void Azuriraj(Proizvod proizvod);
        void ObrisiProizvod(Proizvod proizvod);
        PagedList<Proizvod> VratiProizvodPoKriterijumu(Expression<Func<Models.Proizvod, bool>> filter, ProductParameters proizvodParameters);
    }
}
