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
    public class NotificationUserRepo : RepoBase<NotificationUsers>, INotificationUserRepo
    {
        public NotificationUserRepo(SecurityGateApvDbContext context) : base(context)
        {
        }
    }
}
