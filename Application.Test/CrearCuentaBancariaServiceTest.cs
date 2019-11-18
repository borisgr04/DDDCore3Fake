using Application.Test.Fake;
using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Application.Test
{
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
            var request = new CrearCuentaBancariaRequest { Numero = "1111", Nombre = "aaaaa", TipoCuenta = "Ahorro", Email="test@email.com" };
            CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(_context), new EmailSenderFake());
            var response = _service.Ejecutar(request);
            Assert.AreEqual("Se creó con éxito la cuenta 1111.", response.Mensaje);
        }
    }
}