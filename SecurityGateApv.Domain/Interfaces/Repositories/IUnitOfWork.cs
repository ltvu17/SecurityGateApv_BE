using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public Task<bool> CommitAsync();
    }
}
