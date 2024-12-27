using SecurityGateApv.Application.DTOs.Res;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.Services.Interface
{
    public interface INotificationsService
    {
        public Task<Result<IEnumerable<GetNotificationRes>>> GetNotificationOfUser(int userId, int pageSize, int pageNumber);
        public Task<Result<bool>> MarkNotificationRead(int notificationUserId);
    }
}
