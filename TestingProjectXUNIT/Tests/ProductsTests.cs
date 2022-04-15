using System;
using System.Collections.Generic;
using Moq;
using AutoMapper;
using Product.Models;
using Xunit;
using Product.Controllers;
using Product.Data;
using Product.Profiles;
using Microsoft.AspNetCore.Mvc;
using Product.DTOs;
using Newtonsoft.Json.Serialization;
using Product.Models.Parameters;
using Product.Models.Helpers;
using Product.Logging;

namespace TestingProjectXUNIT.Tests
{
    public class ProductsTests : IDisposable
    {
        Mock<IProductRepository> mockRepo;
        ProizvodProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;
        ProductParameters proizvodParameters;
        ILog logger;
        public ProductsTests()
        {
            mockRepo = new Mock<IProductRepository>();
            realProfile = new ProizvodProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
            proizvodParameters = new ProductParameters();
            logger = new Log();
        }

        public void Dispose()
        {
            mockRepo = null;
            mapper = null;
            configuration = null;
            realProfile = null;
        
        }
        
        [Fact]
        public void GetProductByID_Returns404NotFound_WhenNonExistentIDProvided()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetProductById(0)).Returns(() => null);

            var controller = new ProductsController(mockRepo.Object, mapper, logger);
          
            //Act
            var result = controller.GetProductById(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

  

        [Fact]
        public void GetProductByID_Returns200OK__WhenValidIDProvided()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetProductById(11)).Returns(new Proizvod {

                  Id = 11,
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
              });

            var controller = new ProductsController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.GetProductById(11);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetProductByID_ReturnsCorrectResouceType_WhenValidIDProvided()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetProductById(11)).Returns(new Proizvod
              {

                  Id = 11,
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
              });

            var controller = new ProductsController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.GetProductById(11);

            //Assert
            Assert.IsType<ActionResult<ProizvodReadDTO>>(result);
        }


        [Fact]
        public void GetProductByName_Returns404NotFound_WhenNonExistentNameProvided()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetByCriteria(x => x.Naziv == "Proizvod 22", proizvodParameters)).Returns(() => null);

            var controller = new ProductsController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.GetByName("Proizvod 22", proizvodParameters);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public void GetProductByPrice_Returns404NotFound_WhenNonExistentPriceProvided()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetByCriteria(x => x.Cena == 7889, proizvodParameters)).Returns(() => null);

            var controller = new ProductsController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.GetByPrice(7889, proizvodParameters);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetProductByPDV_Returns404NotFound_WhenNonExistentPDVProvided()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetByCriteria(x => x.Pdv == 2, proizvodParameters)).Returns(() => null);

            var controller = new ProductsController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.GetByPDV(2, proizvodParameters);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetProductByMeasurmentUnit_Returns404NotFound_WhenNonExistentMeasurmentUnitProvided()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetByCriteria(x => x.JedinicaMere.Id == 22, proizvodParameters)).Returns(() => null);

            var controller = new ProductsController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.GetByMeasurementUnit(22, proizvodParameters);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetProductByType_Returns404NotFound_WhenNonExistentTypeProvided()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetByCriteria(x => x.TipProizvoda.Id== 2, proizvodParameters)).Returns(() => null);

            var controller = new ProductsController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.GetByType(2, proizvodParameters);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
       
      
        [Fact]
        public void CreateProduct_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetProductById(11)).Returns(new Proizvod {
                  Id = 11,
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
              });

            var controller = new ProductsController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.PostProduct(new ProizvodCreateAndUpdateDTO { });

            //Assert
            Assert.IsType<ActionResult<ProizvodReadDTO>>(result);
        }

       
        [Fact]
        public void CreateProduct_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetProductById(11)).Returns(new Proizvod
              {
                  Id = 11,
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
              });

            var controller = new ProductsController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.PostProduct(new ProizvodCreateAndUpdateDTO { });

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }


        [Fact]
        public void UpdateProduct_Returns204NoContent_WhenValidObjectSubmitted()
        {
            //Arrange 
            mockRepo.Setup(repo =>
             repo.GetProductById(15)).Returns(new Proizvod
             {
                 Id = 15,
                 Naziv = "Proizvod 15 update",
                 Cena = 1110,
                 Pdv = 10,
                 JedinicaMere = new JedinicaMere
                 {
                     Id = 1,
                     Naziv = "Kilogram"
                 },
                 TipProizvoda = new TipProizvoda
                 {
                     Id = 1,
                     Naziv = "Tip 1"
                 },
                 Dobavljaci = new List<ProizvodDobavljac>
               {
                    new ProizvodDobavljac{
                        Dobavljac = new Dobavljac
                        {
                            Id = 1,
                            PIB = "1221223344",
                            Napomena = "Napomena....",
                            Naziv = "Dobavljac 1"
                        }
                    },
                     new ProizvodDobavljac{
                        Dobavljac = new Dobavljac
                        {
                            Id = 2,
                            PIB = "9874512",
                            Napomena = "Neka napomena...",
                            Naziv = "Dobavljac 2"
                        }
                    }
               }
             });

            var controller = new ProductsController(mockRepo.Object, mapper, logger);
            //Act
            var result = controller.PutProizvod(15, new ProizvodCreateAndUpdateDTO { });

            //Assert
            Assert.IsType<OkResult>(result);
        }


       
        [Fact]
        public void UpdateProduct_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.GetProductById(0)).Returns(() => null);

            var controller = new ProductsController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.PutProizvod(0, new ProizvodCreateAndUpdateDTO { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }


        private PagedList<Proizvod> GetProizvods(int num, ProductParameters proizvodParameters)
        {
            var products = new List<Proizvod>();
            if (num > 0)
            {
                products.Add(new Proizvod
                {
                    Id = 11,
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

                });
            }
            return PagedList<Proizvod>.ToPagedList(products, proizvodParameters.PageNumber, proizvodParameters.PageSize);
        }
    }
}
