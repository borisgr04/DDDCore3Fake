using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class MovimientoFinanciero : Entity<int>
    {
        public CuentaBancaria CuentaBancaria { get; set; }
        public double ValorRetiro { get; set; }
        public double ValorConsignacion { get; set; }
        public DateTime FechaMovimiento { get; set; }
    }
}
