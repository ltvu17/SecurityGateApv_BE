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
    public class VisitorRepo : RepoBase<Visitor>, IVisitorRepo
    {
        private readonly SecurityGateApvDbContext _context;
        private readonly DbSet<Visit> _dbSet;

        public VisitorRepo(SecurityGateApvDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Visit>();
        }

    }
}
