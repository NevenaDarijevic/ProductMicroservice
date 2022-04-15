using AutoMapper;
using Moq;
using NUnit.Framework;
using Product.Data;
using Product.DTOs;
using Product.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MikroservisProizvod.Test.ProizvodTests
{
    public class UpdateProizvodTest
    {
        private Mock<IProizvodRepozitorijum> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private IUpdateProizvodCommand _updateProizvodCommand;
        //private ProizvodDto proizvodToUpdate;
        private Proizvod mappedProizvod;
        private Proizvod proizvodFromDatabase;
        private ProizvodReadDTO mappedProizvodForReturn;
        private Proizvod updatedProizvodFromDb;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IProizvodRepozitorijum>();
            _mockMapper = new Mock<IMapper>();
            _updateProizvodCommand = new UpdateProizvodCommand(_mockGenericRepository.Object, _mockMapper.Object, _mockValidator.Object);
          
            mappedProizvod = new Proizvod
            {
                Id = 1,
                Naziv = "Proizvod 1 update",
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

            proizvodFromDatabase = new Proizvod
            {
                Id = 1,
                Naziv = "Proizvod 1",
                Cena = 11.1,
                Pdv = 0.11,
            };

            updatedProizvodFromDb = new Proizvod
            {
                Id = 1,
                Naziv = "Proizvod 1 update",
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

            mappedProizvodForReturn = new ProizvodReadDTO
            {
                Id = 1,
                Naziv = "Proizvod 1 update",
                Cena = 11.1,
                Pdv = 0.11,
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
        }

        [Test]
        public void UpdateProizvod()
        {
            // priprema

            ProizvodCUDTO proizvodToUpdate = new ProizvodCUDTO
            {
                Id = 1,
                Naziv = "Proizvod 1 update",
                Cena = 11.1,
                Pdv = 0.11,
                JedinicaMereId = 1,
                TipProizvodaId = 1,
                Dobavljaci = new List<long> { 1 }
            };

            _mockRepository.Setup(gr => gr.VratiProizvodPoId(p => p.Id == proizvodToUpdate.Id, ""))
                .Returns(proizvodFromDatabase);

            _mockRepository.Setup(gr => gr.Azuriraj(mappedProizvod))
                .Callback(() => { mappedProizvod.Naziv = "Proizvod 1 update"; });

            _mockMapper.Setup(m => m.Map<Proizvod>(proizvodToUpdate))
                .Returns(mappedProizvod);

            _mockMapper.Setup(m => m.Map<ProizvodReadDTO>(updatedProizvodFromDb))
                  .Returns(mappedProizvodForReturn);

            _mockRepository.Setup(gr => gr.VratiProizvodPoId(p => p.Id == proizvodToUpdate.Id, "JedinicaMere,TipProizvoda,Dobavljaci.Dobavljac"))
                   .Returns(updatedProizvodFromDb);     

            // izvrsenje
            var result = _updateProizvodCommand.Execute(proizvodToUpdate);

            // provera
            _mockRepository.Verify(gr => gr.Azuriraj(mappedProizvod), Times.Once());
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Naziv, proizvodToUpdate.Naziv);

        }

        [Test]
        public void UpdateProizvod_NotExist()
        {
            // priprema
            ProizvodCUDTO proizvodToUpdate = new ProizvodCUDTO
            {
                Id = 1,
                Naziv = "Proizvod 1 update",
                Cena = 11.1,
                Pdv = 0.11,
                JedinicaMereId = 1,
                TipProizvodaId = 1,
                Dobavljaci = new List<long> { 1 }
            };

            _mockRepository.Setup(gr => gr.VratiProizvodPoId(p => p.Id == proizvodToUpdate.Id))
                .Returns((Proizvod)null);

            // izvrsenje i provera
            Exception ex = Assert.Throws<EntityNotFoundException>(delegate { _updateProizvodCommand.Execute(proizvodToUpdate); });
            Assert.That(ex.Message, Is.EqualTo("Nije moguce izvrsiti azuriranje proizvoda jer ne postoji."));

        }
    }
}
