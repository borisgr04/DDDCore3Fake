using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public interface IServicioFinanciero
    {

        string Nombre { get; set; }
        string Numero { get; set; }
        double Saldo { get; }

        void Retirar(double valor);
        void Consignar(double valor);
        void Trasladar(IServicioFinanciero servicioFinanciero, double valor);

    }
}
