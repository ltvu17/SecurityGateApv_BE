using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.Services.Interface
{
    public interface IScriptService
    {
        public Task<Result<bool>> Coflow1_1_CreateVisit(int requestOfStaff, int requestOfSecurity);
        public Task<Result<bool>> Coflow1_2_RejectVisitOfSecurity(int requestId);
        public Task<Result<bool>> Coflow1_3_CancelVisit(int requestId);
        public Task<Result<bool>> Coflow1_1_MockVisitor(int numberOfVisitor);

        public Task<Result<bool>> Coflow2_1_CreateSchedule(int requestOfSchedule);
        public Task<Result<bool>> Coflow2_2_AssignScheduleForStaff(int requestId);
        public Task<Result<bool>> Coflow2_3_CreateScheduleAssign(int numberOfSchedule);
        public Task<Result<bool>> Coflow2_4_AcceptAndRejectTask(int numberOfReject, int numberOfAccept);

        public Task<Result<bool>> Coflow3_1_Check_in(int numberOfCheckIn);
        public Task<Result<bool>> Coflow4_GetListCheckIn();
        public Task<Result<bool>> Coflow4_1_Check_out(int checkInId);
        public Task<Result<bool>> Coflow4_2_Check_out_lost_card(int checkInId);

    }
}
