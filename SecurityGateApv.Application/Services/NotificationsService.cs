using AutoMapper;
using SecurityGateApv.Application.DTOs.Res;
using SecurityGateApv.Application.Services.Interface;
using SecurityGateApv.Domain.Errors;
using SecurityGateApv.Domain.Interfaces.Repositories;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.Services
{
    public class NotificationsService : INotificationsService
    {
        private readonly INotificationUserRepo _notificationUserRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsService(INotificationUserRepo notificationUserRepo, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _notificationUserRepo = notificationUserRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<IEnumerable<GetNotificationRes>>> GetNotificationOfUser(int userId, int pageSize, int pageNumber)
        {
            var notification = await _notificationUserRepo.FindAsync(s => s.ReceiverID == userId, pageSize, pageNumber,orderBy: s => s.OrderBy(t=>t.Notification.SentDate) , includeProperties: "Notification, Notification.NotificationType, Sender");
            if(notification.Count() == 0)
            {
                return Result.Failure<IEnumerable<GetNotificationRes>>(Error.NotFound);
            }
            return (_mapper.Map<IEnumerable<GetNotificationRes>>(notification)).ToList();
        }

        public async Task<Result<bool>> MarkNotificationRead(int notificationUserId)
        {
            var notificationUser = (await _notificationUserRepo.FindAsync(s => s.NotificationUserID == notificationUserId)).FirstOrDefault();
            if (notificationUser == null) {
                return Result.Failure<bool>(Error.NotFound);
            }
            notificationUser.MarkAsRead();
            await _notificationUserRepo.UpdateAsync(notificationUser);
            var commit = await _unitOfWork.CommitAsync();
            if (!commit)
            {
                return Result.Failure<bool>(Error.CommitError);
            }
            return true;
        }
    }
}
