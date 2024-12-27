using SecurityGateApv.Application.DTOs.Res;
using SecurityGateApv.Domain.Models;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.Services.Interface
{
    public interface IDashboardService
    {
        public Task<Result<DashBoardVisitRes>> GetVisit(string token);
        public Task<Result<DashboardUserRes>> GetUser();
        public Task<Result<DashboardVisitorRes>> GetVisitor();
        public Task<Result<DashboardSchedule>> GetSchedule();
        public Task<Result<DashboardMission>> GetMission(string token);
        public Task<Result<VisitorSessionCountRes>> GetVisitorSessionCountByYear(int year, string token);
        public Task<Result<VisitorSessionCountMonthRes>> GetVisitorSessionCountByMonth(int year, int month, string token);
        Task<Result<List<GetVisitorSessionRes>>> GetRecentVisitorSessions(string token, int count = 5);
        Task<Result<List<VisitorSessionStatusCountRes>>> GetVisitorSessionCountByStatusForToday(string token);
        Task<Result<List<CardStatusCountRes>>> GetCardCountByStatus();
        Task<Result<GetCardCountIssueRes>> GetCardBountByIssue();

    }
}
