using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req
{
    public class QueryParameters<T>
    {
        public Expression<Func<T, bool>> Filter { get; set; }
        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; set; }
        public string IncludeProperties { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }

}
