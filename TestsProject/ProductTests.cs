using Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestsProject
{
    public class ProductTests:IDisposable
    {
        Proizvod testProizvod;

        public ProductTests()
        {
            testProizvod = new Proizvod
            {
               
                Naziv = "Proizvod 1",
                Cena = 11.1,
                Pdv = 0.11,
                JedinicaMereId = 1,
                TipProizvodaId = 1,
                Dobavljaci = new List<long> { 1 }
            };
        }

        public void Dispose()
        {
            testProizvod = null;
        }


        [Fact]
        public void CanChangeHowTo()
        {
            //Arrange

            //Act
            testCommand.HowTo = "Execute Unit Tests";

            //Assert
            Assert.Equal("Execute Unit Tests", testCommand.HowTo);

        }

        [Fact]
        public void CanChangePlatform()
        {
            //Arrange

            //Act
            testCommand.Platform = "xUnit";

            //Assert
            Assert.Equal("xUnit", testCommand.Platform);

        }

        [Fact]
        public void CanChangeCommandLine()
        {
            //Arrange

            //Act
            testCommand.CommandLine = "dotnet test";

            //Assert
            Assert.Equal("dotnet test", testCommand.CommandLine);

        }

    }
}
}
