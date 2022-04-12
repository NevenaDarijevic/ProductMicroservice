using Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Data
{
     public interface IProizvodRepozitorijum
    {
        IEnumerable<Proizvod> VratiProizvode();
        Proizvod VratiProizvodPoId(long id);
    }
}
