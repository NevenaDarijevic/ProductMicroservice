using Product.Models;
using Product.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Product.Data
{
    //MockProizvodRepozitorijum klasa implementira interfejs IProizvodRepozitorijum gde nam se nalaze sve metode za funkcionalne zahteve
    //S obzirom da se radi o Mock repozitorijumum(fake data), ovde ce biti hardkodovani podaci
    public class MockProizvodRepozitorijum : IProizvodRepozitorijum
    {
        public void Azuriraj(Proizvod proizvod)
        {
            throw new NotImplementedException();
        }

        public void KreirajProizvod(Proizvod proizvod)
        {
            throw new NotImplementedException();
        }

        public void ObrisiProizvod(Proizvod proizvod)
        {
            throw new NotImplementedException();
        }

        public bool SacuvajPromene()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Proizvod> VratiProizvode(ProizvodParameters proizvodParameters)
        {
            var proizvodi = new List<Proizvod> {

               new Proizvod
               { 
                Id = 1,
                Naziv = "Proizvod 1",
                Cena = 11.1,
                Pdv = 0.11,
                JedinicaMere = new JedinicaMere
                {
                    Id = 1,
                    Naziv = "Jedinica mere 1"
                },
                TipProizvoda = new TipProizvoda
                {
                    Id = 1,
                    Naziv = "Tip proizvoda 1"
                },
                Dobavljaci = new List<ProizvodDobavljac>()
                {
                    new ProizvodDobavljac{
                        Dobavljac = new Dobavljac
                        {
                            Id = 1,
                            Naziv = "Dobavljac 1",
                            PIB = "123554",
                            Napomena = "Napomena1"
                        }
                    }
                } 
               },
                  new Proizvod
                {
                    Id = 2,
                    Naziv = "Proizvod 2",
                    Cena = 1050,
                    Pdv = 20,
                    JedinicaMere = new JedinicaMere
                {
                    Id = 2,
                    Naziv = "Jedinica mere 2"
                },
                TipProizvoda = new TipProizvoda
                {
                    Id = 1,
                    Naziv = "Tip proizvoda 1"
                },
                Dobavljaci = new List<ProizvodDobavljac>()
                {
                    new ProizvodDobavljac{
                        Dobavljac = new Dobavljac
                        {
                            Id = 2,
                            Naziv = "Dobavljac 2",
                            PIB = "1234",
                            Napomena = "Napomena"
                        }
                    }
                }
                },
                   new Proizvod
                {
                    Id = 3,
                    Naziv = "Proizvod 3",
                    Cena = 100,
                    Pdv = 50,
                   JedinicaMere = new JedinicaMere
                {
                    Id = 1,
                    Naziv = "Jedinica mere 1"
                },
                TipProizvoda = new TipProizvoda
                {
                    Id = 2,
                    Naziv = "Tip proizvoda 2"
                },
                Dobavljaci = new List<ProizvodDobavljac>()
                {
                    new ProizvodDobavljac{
                        Dobavljac = new Dobavljac
                        {
                            Id = 1,
                            Naziv = "Dobavljac 3",
                            PIB = "331234",
                            Napomena = "Napomena"
                        }
                    }
                }
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
                JedinicaMere = new JedinicaMere
                {
                    Id = 1,
                    Naziv = "Jedinica mere 1"
                },
                TipProizvoda = new TipProizvoda
                {
                    Id = 2,
                    Naziv = "Tip proizvoda 2"
                },
                Dobavljaci = new List<ProizvodDobavljac>()
                {
                    new ProizvodDobavljac{
                        Dobavljac = new Dobavljac
                        {
                            Id = 1,
                            Naziv = "Dobavljac 3",
                            PIB = "331234",
                            Napomena = "Napomena"
                        }
                    }
                }
            };
        }

      
        public IEnumerable<Proizvod> VratiProizvodPoKriterijumu(Expression<Func<Proizvod, bool>> filter, ProizvodParameters proizvodParameters)
        {
            return null;
        }

      
    }
}
