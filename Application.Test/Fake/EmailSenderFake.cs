using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.Fake
{
    class EmailSenderFake : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"Se ha enviado el correo Fake satisfactoriamente. a {email}");
            return Task.CompletedTask;
        }
    }
    /// <summary>
    /// No se usa en las test, solo es un ejemplo de Doble de Prueba con Valores Predefinidos
    /// </summary>
    public class CuentasBancariasServiceStub
    {
        public List<CuentaBancaria> Get(string tipo)
        {
            if(tipo=="corriente") return new List<CuentaBancaria>() { new CuentaCorriente  {  Numero="11", Nombre="222"  }      };
            return new List<CuentaBancaria>() { new CuentaAhorro { Numero = "11", Nombre = "222" } };
        }
    }
}
