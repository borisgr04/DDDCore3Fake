using Domain.Entities;
using NUnit.Framework;

namespace Domain.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConsignacionTest()
        {
            var cuenta = new CuentaAhorro();
            cuenta.Numero = "111";
            cuenta.Nombre = "Ahorro Ejemplo";
            cuenta.Consignar(10000);
            Assert.AreEqual(10000,cuenta.Saldo);
        }
    }
}