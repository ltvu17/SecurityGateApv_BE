using SecurityGateApv.Application.DTOs.Req.CreateReq;
using SecurityGateApv.Application.DTOs.Res;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.Services.Interface
{
    public interface IScheduleUserService
    {
        public Task<Result<ICollection<GetScheduleRes>>> GetAllSchedule(int pageNumber, int pageSize);
        public Task<Result<ScheduleUserRes>> GetScheduleUserById(int scheduleUserId);
        public Task<Result<int>> GetScheduleUserByStaffId(int staffId);
        public Task<Result<CreateScheduleUserCommand>> CreateScheduleUser(CreateScheduleUserCommand command);
        public Task<Result<List<GetScheduleUserRes>>> GetScheduleUserByUserIdAndStatus(int staffId,string status, int pageNumber, int pageSize);
        public Task<Result<List<GetScheduleUserRes>>> GetScheduleUserByUserId(int userId, int pageNumber, int pageSize);
        public Task<Result<bool>> RejectScheduleUser(int scheduleId);
        public Task<Result<bool>> AproveScheduleUser(int scheduleId);
        public Task<Result<bool>> CancelScheduleUser(int scheduleId);


    }
}
