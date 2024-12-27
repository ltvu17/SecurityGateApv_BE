using Microsoft.EntityFrameworkCore;
using SecurityGateApv.Domain.Interfaces.Repositories;
using SecurityGateApv.Domain.Models;
using SecurityGateApv.Infras.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Infras.Repositories
{
    public class VisitorSessionRepo : RepoBase<VisitorSession>, IVisitorSessionRepo
    {
        private readonly SecurityGateApvDbContext _context;
        private readonly DbSet<Visit> _dbSet;

        public VisitorSessionRepo(SecurityGateApvDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Visit>();
        }

        public Task<IEnumerable<VisitorSession>> FindWithProjectionAsync(Expression<Func<VisitorSession, bool>> filter, int pageSize, int pageNumber, Func<IQueryable<VisitorSession>, IOrderedQueryable<VisitorSession>> orderBy, string includeProperties)
        {
            //IQueryable<VisitorSession> query = _context.Set<VisitorSession>();

            //if (filter != null)
            //{
            //    query = query.Where(filter);
            //}

            //foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            //{
            //    query = query.Include(includeProperty);
            //}

            //if (orderBy != null)
            //{
            //    query = orderBy(query);
            //}

            //query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            //return await query.Select(s => new VisitorSession
            //{
            //    VisitorSessionId = s.VisitorSessionId,
            //    CheckinTime = s.CheckinTime,
            //    VisitDetailId = s.VisitDetailId,
            //    SecurityIn = new Security
            //    {
            //        Name = s.SecurityIn.Name
            //    }
            //}).ToListAsync();
            throw new NotImplementedException();
        }
    }
}
