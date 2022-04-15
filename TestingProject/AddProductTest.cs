using AutoMapper;
using Moq;
using NUnit.Framework;
using Product.Data;
using Product.DTOs;
using Product.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestingProject
{
    public class Tests
    {
        private Mock<IProizvodRepozitorijum> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private ProizvodCUDTO proizvodToAdd;
        private ProizvodReadDTO mappedProizvodForReturn;
        private Proizvod proizvodFromDB;
        private DBProizvodRepozitorijum _repository;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IProizvodRepozitorijum>();
            _mockMapper = new Mock<IMapper>();

            proizvodToAdd = new ProizvodCUDTO
            {
               
                Naziv = "Proizvod 16",
                Cena = 1223.2,
                Pdv = 0.3,
                JedinicaMereId = 1,
                TipProizvodaId = 1,
                Dobavljaci = new List<long> { 1 }
            };
            mappedProizvodForReturn = new ProizvodReadDTO
            {
                Id = 16,
                Naziv = "Proizvod 16",
                Cena = 1223.2,
                Pdv = 0.3,
                JedinicaMere = new JedinicaMereDTO
                {
                    Id = 1,
                    Naziv = "Jedinica mere 1"
                },
                TipProizvoda = new TipProizvodaDTO
                {
                    Id = 1,
                    Naziv = "Tip proizvoda 1"
                },
                Dobavljaci = new List<DobavljacDTO>
                {

                    new DobavljacDTO
                    {
                        Id = 1,
                        Naziv = "Dobavljac 1"
                    }
                }
            };
            proizvodFromDB = new Proizvod
            {
                Id = 16,
                Naziv = "Proizvod 1",
                Cena = 1223.2,
                Pdv = 0.3,
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
                Dobavljaci = new List<ProizvodDobavljac>
                {
                    new ProizvodDobavljac{
                        Dobavljac = new Dobavljac
                        {
                            Id = 1,
                            PIB = "123",
                            Napomena = "Napomena",
                            Naziv = "Dobavljac 1"
                        }
                    }
                }
            };

        }

        [Test]
        public void AddProizvod()
        {
            // ARANGE
            long newId = 16;

            Proizvod mappedProizvod = new Proizvod
            {
                Id = 0,
                Naziv = "Proizvod 16",
                Cena = 1223.2,
                Pdv = 0.3,
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
                Dobavljaci = new List<ProizvodDobavljac>
                {
                    new ProizvodDobavljac{
                        Dobavljac = new Dobavljac
                        {
                            Id = 1,
                            PIB = "123",
                            Napomena = "Napomena",
                            Naziv = "Dobavljac 1"
                        }
                    }
                }
            };

           

            _mockMapper.Setup(m => m.Map<Proizvod>(proizvodToAdd))
                .Returns(mappedProizvod);

            _mockMapper.Setup(m => m.Map<ProizvodReadDTO>(proizvodFromDB))
                .Returns(mappedProizvodForReturn);

            _mockRepository.Setup(gr => gr.KreirajProizvod(mappedProizvod))
                .Callback(() => { mappedProizvod.Id = newId; });

            _mockRepository.Setup(gr => gr.VratiProizvodPoId(mappedProizvod.Id))
                .Returns(proizvodFromDB);

            // ACT
            var result = _mockRepository.;

            // ASSERT
            _mockRepository.Verify(gr => gr.KreirajProizvod(mappedProizvod), Times.Once());
            Assert.IsNotNull(result);
            Assert.AreEqual(newId, result.Id);

        }
    }
}