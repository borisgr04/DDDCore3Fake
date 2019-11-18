using Domain.Contracts;
using Domain.Entities;
using Domain.Repositories;
using System;

namespace Application
{
    public class ConsignarService 
    {
        readonly IUnitOfWork _unitOfWork;
        
        public ConsignarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ConsignarResponse Ejecutar(ConsignarRequest request)
        {
            var cuenta = _unitOfWork.CuentaBancariaRepository.FindFirstOrDefault(t => t.Numero==request.NumeroCuenta);
            if (cuenta != null)
            {
                cuenta.Consignar(request.Valor);
                _unitOfWork.Commit();
                return new ConsignarResponse() { Mensaje = $"Su Nuevo saldo es {cuenta.Saldo}." };
            }
            else
            {
                return new ConsignarResponse() { Mensaje = $"Número de Cuenta No Válido." };
            }
        }
    }
    public class ConsignarRequest
    {
        public string NumeroCuenta { get; set; }
        public double Valor { get; set; }
    }
    public class ConsignarResponse
    {
        public string Mensaje { get; set; }
    }
}
