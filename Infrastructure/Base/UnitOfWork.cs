using Domain.Contracts;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Base
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private IDbContext _dbContext;

        private ICuentaBancariaRepository _cuentaBancariaRepository;
        public ICuentaBancariaRepository CuentaBancariaRepository { get { return _cuentaBancariaRepository ?? (_cuentaBancariaRepository = new CuentaBancariaRepository(_dbContext)); } }


        public UnitOfWork(IDbContext context)
        {
            _dbContext = context;
        }
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
        }
        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing && _dbContext != null)
            {
                ((DbContext)_dbContext).Dispose();
                _dbContext = null;
            }
        }

    }
}
