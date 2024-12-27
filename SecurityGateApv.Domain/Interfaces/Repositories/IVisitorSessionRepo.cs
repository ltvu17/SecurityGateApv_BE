using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Interfaces.Repositories
{
    public interface IVisitorSessionRepo : IRepoBase<VisitorSession>
    {
        Task<IEnumerable<VisitorSession>> FindWithProjectionAsync(Expression<Func<VisitorSession, bool>> filter, int pageSize, int pageNumber, Func<IQueryable<VisitorSession>, IOrderedQueryable<VisitorSession>> orderBy, string includeProperties);
    }
}
