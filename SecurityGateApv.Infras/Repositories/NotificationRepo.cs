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
    public class NotificationRepo : RepoBase<Notification>, INotificationRepo
    {
        public NotificationRepo(SecurityGateApvDbContext context) : base(context)
        {
        }
    }
}
