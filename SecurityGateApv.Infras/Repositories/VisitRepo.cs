using Microsoft.EntityFrameworkCore;
using SecurityGateApv.Domain.Interfaces.Repositories;
using SecurityGateApv.Domain.Models;
using SecurityGateApv.Infras.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Infras.Repositories
{
    public class VisitRepo : RepoBase<Visit>, IVisitRepo
    {
        private readonly SecurityGateApvDbContext _context;
        private readonly DbSet<Visit> _dbSet;

        public VisitRepo(SecurityGateApvDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Visit>();
        }

        public async Task<Visit> GetIdAsNoTracking(int id)
        {
            return await _dbSet.Where(s => s.VisitId == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Visit>> GetAllVisitIncludeVisitor()
        {
            return await _dbSet
                .Include(v => v.VisitDetail)
                    .ThenInclude(vd => vd.Visitor)
                .ToListAsync();
        }
    }
}
