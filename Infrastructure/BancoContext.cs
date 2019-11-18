using Domain.Entities;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class BancoContext : DbContextBase
    {
        public BancoContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<CuentaAhorro> CuentasAhorro { get; set; }
        public DbSet<CuentaCorriente> CuentasCorriente { get; set; }


    }
}
