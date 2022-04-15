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
    public class FindProizvodTest
    {
        private Mock<DBProizvodRepozitorijum> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private IFindProizvodCommand _findProizvodService;
        private Proizvod proizvod;
        private ProizvodReadDTO proizvodDto;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<DBProizvodRepozitorijum>();
            _mockMapper = new Mock<IMapper>();
            _findProizvodService = new FindProizvodCommand(_mockGenericRepository.Object, _mockMapper.Object);

            proizvod = new Proizvod
            {
                Id = 11,
                Naziv = "Proizvod 1",
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

            proizvodDto = new ProizvodReadDTO
            {
                Id = 1,
                Naziv = "Proizvod 1",
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
        public void FindProizvod()
        {
            // priprema
            long id = 1;

            _mockRepository.Setup(gr => gr.VratiProizvodPoId(p => p.Id == id, "JedinicaMere,TipProizvoda,Dobavljaci.Dobavljac"))
                .Returns(proizvod);
            _mockMapper.Setup(m => m.Map<ProizvodReadDTO>(proizvod))
                .Returns(proizvodDto);

            // izvrsenje
            var res = _findProizvodService.Execute(id);

            // provera
            Assert.IsNotNull(res);
        }

        [Test]
        public void FindProizvod_NotExist()
        {
            long id = 11;

            _mockRepository.Setup(gr => gr.VratiProizvodPoId(id))
                .Returns((Proizvod)null);

            Exception ex = Assert.Throws<Exception>(delegate { _findProizvodService.Execute(id); });
            Assert.That(ex.Message, Is.EqualTo("Trazeni proizvod nije pronadjen."));
        }
    }
}
