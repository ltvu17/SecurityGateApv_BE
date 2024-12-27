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
    public class VisitCardRepo : RepoBase<VisitCard>, IVisitCardRepo
    {
        private readonly SecurityGateApvDbContext _context;
        private readonly DbSet<VisitCard> _dbSet;

        public VisitCardRepo(SecurityGateApvDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<VisitCard>();
        }
    
    }
}
