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
        public void GetAllProducts_ReturnsZeroResources_WhenDBIsEmpty()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.VratiProizvode(proizvodParameters)).Returns(GetProizvods(0,proizvodParameters));

            var controller = new ProductssController(mockRepo.Object, mapper,logger);

            //Act
            var result = controller.GetProizvod(proizvodParameters);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

      
        [Fact]
        public void GetAllProducts_ReturnsOneResource_WhenDBHasOneResource()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.VratiProizvode(proizvodParameters)).Returns(GetProizvods(1, proizvodParameters));

            var controller = new ProductssController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.GetProizvod(proizvodParameters);

            //Assert
            var okResult = result.Result as OkObjectResult;

            var products = okResult.Value as List<ProizvodReadDTO>;

            Assert.Single(products);
        }

    
        [Fact]
        public void GetAllProducts_Returns200OK_WhenDBHasOneResource()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.VratiProizvode(proizvodParameters)).Returns(GetProizvods(1, proizvodParameters));

            var controller = new ProductssController(mockRepo.Object, mapper, logger);


            //Act
            var result = controller.GetProizvod(proizvodParameters);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

        }

  
        [Fact]
        public void GetAllProducts_ReturnsCorrectType_WhenDBHasOneResource()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.VratiProizvode(proizvodParameters)).Returns(GetProizvods(1, proizvodParameters));

            var controller = new ProductssController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.GetProizvod(proizvodParameters);

            //Assert
            Assert.IsType<ActionResult<IEnumerable<ProizvodReadDTO>>>(result);
        }

     
           
        [Fact]
        public void GetProductByID_Returns404NotFound_WhenNonExistentIDProvided()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.VratiProizvodPoId(0)).Returns(() => null);

            var controller = new ProductssController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.GetProizvod(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

      
        [Fact]
        public void GetProductByID_Returns200OK__WhenValidIDProvided()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.VratiProizvodPoId(11)).Returns(new Proizvod {

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

            var controller = new ProductssController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.GetProizvod(11);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

      
        [Fact]
        public void GetProductByID_ReturnsCorrectResouceType_WhenValidIDProvided()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.VratiProizvodPoId(11)).Returns(new Proizvod {

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

            var controller = new ProductssController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.GetProizvod(11);

            //Assert
            Assert.IsType<ActionResult<ProizvodReadDTO>>(result);
        }

      
        [Fact]
        public void CreateProduct_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.VratiProizvodPoId(11)).Returns(new Proizvod {
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

            var controller = new ProductssController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.PostProizvod(new ProizvodCUDTO { });

            //Assert
            Assert.IsType<ActionResult<ProizvodReadDTO>>(result);
        }

       
        [Fact]
        public void CreateProduct_Returns201Created_WhenValidObjectSubmitted()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.VratiProizvodPoId(11)).Returns(new Proizvod
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

            var controller = new ProductssController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.PostProizvod(new ProizvodCUDTO { });

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }


        [Fact]
        public void UpdateProduct_Returns204NoContent_WhenValidObjectSubmitted()
        {
            //Arrange 
            mockRepo.Setup(repo =>
             repo.VratiProizvodPoId(11)).Returns(new Proizvod
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

            var controller = new ProductssController(mockRepo.Object, mapper, logger);
            //Act
            var result = controller.PutProizvod(1, new ProizvodCUDTO { });

            //Assert
            Assert.IsType<NoContentResult>(result);
        }


       
        [Fact]
        public void UpdateProduct_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.VratiProizvodPoId(0)).Returns(() => null);

            var controller = new ProductssController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.PutProizvod(0, new ProizvodCUDTO { });

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public void PartialProductUpdate_Returns404NotFound_WhenNonExistentResourceIDSubmitted()
        {
            //Arrange 
            mockRepo.Setup(repo =>
              repo.VratiProizvodPoId(0)).Returns(() => null);

            var controller = new ProductssController(mockRepo.Object, mapper, logger);

            //Act
            var result = controller.PatchProizvod(0, new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<ProizvodCUDTO> { });

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
