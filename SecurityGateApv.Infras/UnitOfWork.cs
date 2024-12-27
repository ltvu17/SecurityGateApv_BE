using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecurityGateApv.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Infras
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly ILogger<UnitOfWork<TContext>> _logger;
        public UnitOfWork(TContext context, ILogger<UnitOfWork<TContext>> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbUpdateException)
            {
                _logger.LogError(dbUpdateException, "An error occured during commiting changes");
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            // ReSharper disable once GCSuppressFinalizeForTypeWithoutDestructor
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
