using Application;
using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            /*var optionsSqlServer = new DbContextOptionsBuilder<BancoContext>()
             .UseSqlServer("Server=.\\;Database=Banco;Trusted_Connection=True;MultipleActiveResultSets=true")
             .Options;*/

            var optionsInMemory = new DbContextOptionsBuilder<BancoContext>()
             .UseInMemoryDatabase("Banco")
             .Options;

            BancoContext context = new BancoContext(optionsInMemory);
            
            CrearCuentaBancaria(context);
            ConsignarCuentaBancaria(context);
        }

        private static void ConsignarCuentaBancaria(BancoContext context)
        {
            #region  Consignar

            ConsignarService _service = new ConsignarService(new UnitOfWork(context));
            var request = new ConsignarRequest() { NumeroCuenta = "524255", Valor = 1000 };

            ConsignarResponse response = _service.Ejecutar(request);

            System.Console.WriteLine(response.Mensaje);
            #endregion
            System.Console.ReadKey();
        }

        private static void CrearCuentaBancaria(BancoContext context)
        {
            #region  Crear

            /*CrearCuentaBancariaService _service = new CrearCuentaBancariaService(new UnitOfWork(context));
            var requestCrer = new CrearCuentaBancariaRequest() { Numero = "524255", Nombre = "Boris Arturo" };

            CrearCuentaBancariaResponse responseCrear = _service.Ejecutar(requestCrer);

            System.Console.WriteLine(responseCrear.Mensaje);*/
            #endregion
        }
    }
}
