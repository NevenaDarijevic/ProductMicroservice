using Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Product.Data
{
     public interface IProizvodRepozitorijum
    {
        IEnumerable<Proizvod> VratiProizvode();
        Proizvod VratiProizvodPoId(long id);
        void KreirajProizvod(Proizvod proizvod);
        bool SacuvajPromene();
        void Azuriraj(Proizvod proizvod);
        void ObrisiProizvod(Proizvod proizvod);
        IEnumerable<Proizvod> VratiProizvodPoKriterijumu(Expression<Func<Models.Proizvod, bool>> filter);
    }
}
