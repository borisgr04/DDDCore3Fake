using Domain.Contracts;
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
}
