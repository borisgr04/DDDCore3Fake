using Application.Test.Fake;
using Domain.Contracts;
using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Application.Test
{
    /// <summary>
    /// https://danielpalme.github.io/ReportGenerator/usage.html
    /// dotnet add package coverlet.msbuild
    /// dotnet test /p:CollectCoverage=true
    /// Install-Package coverlet.msbuild -Version 2.9.0
    /// https://github.com/coverlet-coverage/coverlet?WT.mc_id=-blog-scottha
    /// coverlet bin\Debug\netcoreapp3.0\Application.Test.dll --target "dotnet" --targetargs "test --no-build"
    /// </summary>
    public class CrearCuentaBancariaServiceTest
    {
        BancoContext _context;

        [SetUp]
        public void Setup()
        {
            /*var optionsSqlServer = new DbContextOptionsBuilder<BancoContext>()
             .UseSqlServer("Server=.\\;Database=Banco;Trusted_Connection=True;MultipleActiveResultSets=true")
             .Options;*/

            var optionsInMemory = new DbContextOptionsBuilder<BancoContext>().UseInMemoryDatabase("Banco").Options;
            _context = new BancoContext(optionsInMemory);
        }

        [Test]
        public void CrearCuentaBancariaTest()
        {
            var request = new CrearCuentaBancariaRequest { Numero = "22222", Nombre = "aaaaa", TipoCuenta = "Ahorro", Email="test@email.com" };
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_context), new EmailSenderFake());
            var response = _service.Ejecutar(request);
            Assert.AreEqual("Se cre� con �xito la cuenta 22222.", response.Mensaje);
        }

        [Test]
        public void CrearCuentaBancariaMoqTest()
        {
            // create mock version
            var mockDependency = new Mock<IEmailSender>();

            // set up mock version's method
            mockDependency.Setup(x => x.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            var request = new CrearCuentaBancariaRequest { Numero = "1111", Nombre = "aaaaa", TipoCuenta = "Ahorro", Email = "test@email.com" };
            CrearCuentaBancariaService _serviceSut = new CrearCuentaBancariaService(new UnitOfWork(_context), mockDependency.Object);
            var response = _serviceSut.Ejecutar(request);

            // Assert that the  method was called with the parameters expecteds
            mockDependency.Verify(x => x.SendEmailAsync(request.Email, "Cuenta Creada!!", $"Se ha creado cuenta bancaria n�mero {request.Numero}"), Times.Once);

            Assert.AreEqual("Se cre� con �xito la cuenta 1111.", response.Mensaje);
        }
    }
}