using Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Data
{
    //MockProizvodRepozitorijum klasa implementira interfejs IProizvodRepozitorijum gde nam se nalaze sve metode za funkcionalne zahteve
    //S obzirom da se radi o Mock repozitorijumum(fake data), ovde ce biti hardkodovani podaci
    public class MockProizvodRepozitorijum : IProizvodRepozitorijum
    {
        public IEnumerable<Proizvod> VratiProizvode()
        {
            var proizvodi = new List<Proizvod> {

                 new Proizvod
                {
                    Id = 1,
                    Naziv = "Proizvod 1",
                    Cena = 100,
                    Pdv = 20,
                    TipProizvodaId =1,
                    JedinicaMereId = 1
                },
                  new Proizvod
                {
                    Id = 2,
                    Naziv = "Proizvod 2",
                    Cena = 1050,
                    Pdv = 20,
                    TipProizvodaId =1,
                    JedinicaMereId = 2
                },
                   new Proizvod
                {
                    Id = 3,
                    Naziv = "Proizvod 3",
                    Cena = 100,
                    Pdv = 50,
                    TipProizvodaId =2,
                    JedinicaMereId = 1
                }
                };

            return proizvodi;
        }

        public Proizvod VratiProizvodPoId(long id)
        {
            return new Proizvod
            {
                Id = 1,
                Naziv = "Proizvod 1",
                Cena = 100,
                Pdv = 20,
                TipProizvodaId =1,
                JedinicaMereId = 1
            };
        }
    }
}
