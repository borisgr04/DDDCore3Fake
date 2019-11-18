using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application
{
    public class CrearCuentaBancariaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;


        public CrearCuentaBancariaService(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }
        public CrearCuentaBancariaResponse Ejecutar(CrearCuentaBancariaRequest request)
        {
            CuentaBancaria cuenta = _unitOfWork.CuentaBancariaRepository.FindFirstOrDefault(t => t.Numero==request.Numero);
            if (cuenta == null)
            {
                CuentaBancaria cuentaNueva = new CuentaAhorro();//Debe ir un factory que determine que tipo de cuenta se va a crear
                cuentaNueva.Nombre = request.Nombre;
                cuentaNueva.Numero = request.Numero;
                cuentaNueva.Email = request.Email;
                _unitOfWork.CuentaBancariaRepository.Add(cuentaNueva);
                _unitOfWork.Commit();
                var result= _emailSender.SendEmailAsync(cuentaNueva.Email, "Cuenta Creada!!", $"Se ha creado cuenta bancaria número {cuentaNueva.Numero}");
                result.Wait();
                return new CrearCuentaBancariaResponse() { Mensaje = $"Se creó con éxito la cuenta {cuentaNueva.Numero}." };
            }
            else
            {
                return new CrearCuentaBancariaResponse() { Mensaje = $"El número de cuenta ya exite" };
            }
        }



    }
    public class CrearCuentaBancariaRequest
    {
        [Required(ErrorMessage ="El nombre es un campo requerido")]
        public string Nombre { get; set; }
        [Required]
        public string TipoCuenta { get; set; }
        [Required]
        public string Numero { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "El Email no tiene el formato adecuado")]
        public string Email { get; set; }
    }
    public class CrearCuentaBancariaResponse
    {
        public string Mensaje { get; set; }
    }
}
