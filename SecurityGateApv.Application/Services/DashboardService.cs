using AutoMapper;
using SecurityGateApv.Application.DTOs.Res;
using SecurityGateApv.Application.Services.Interface;
using SecurityGateApv.Domain.Common;
using SecurityGateApv.Domain.Enums;
using SecurityGateApv.Domain.Errors;
using SecurityGateApv.Domain.Interfaces.Jwt;
using SecurityGateApv.Domain.Interfaces.Repositories;
using SecurityGateApv.Domain.Models;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IVisitRepo _visitRepo;
        private readonly IUserRepo _userRepo;
        private readonly IVisitorRepo _visitorRepo;
        private readonly IScheduleRepo _scheduleRepo;
        private readonly IScheduleUserRepo _scheduleUserRepo;
        private readonly IVisitorSessionRepo _visitorSessionRepo;
        private readonly ICardRepo _cardRepo;
        private readonly IMapper _mapper;
        private readonly IJwt _jwt;

        public DashboardService(IVisitRepo visitRepo, IUserRepo userRepo, IVisitorRepo visitorRepo, IScheduleRepo scheduleRepo, IScheduleUserRepo scheduleUserRepo,
            IVisitorSessionRepo visitorSessionRepo, IMapper mapper, ICardRepo cardRepo, IJwt jwt)
        {
            _visitRepo = visitRepo;
            _userRepo = userRepo;
            _visitorRepo = visitorRepo;
            _scheduleRepo = scheduleRepo;
            _scheduleUserRepo = scheduleUserRepo;
            _visitorSessionRepo = visitorSessionRepo;
            _mapper = mapper;
            _cardRepo = cardRepo;
            _jwt = jwt;
        }

        public DashboardService()
        {
        }

        public async Task<Result<DashboardMission>> GetMission(string token)
        {
            var res = new DashboardMission()
            {
                Total = 0,
                Approved = 0,
                Pending = 0,
                Assigned = 0,
                Expired = 0,
                Rejected = 0,
                Cancel = 0
            };
            var role = _jwt.DecodeJwt(token);
            var schedules = (await _scheduleUserRepo.GetAllAsync());
            if (role == UserRoleEnum.Admin.ToString() || role == UserRoleEnum.Manager.ToString())
            {
                schedules = (await _scheduleUserRepo.GetAllAsync());
            }
            else if (role == UserRoleEnum.DepartmentManager.ToString())
            {
                var userID = _jwt.DecodeJwtUserId(token);
                var dm = (await _userRepo.FindAsync(s => s.UserId == userID)).FirstOrDefault();
                schedules = await _scheduleUserRepo.FindAsync(s => s.Schedule.CreateById == dm.UserId, int.MaxValue);
            }
            else if (role == UserRoleEnum.Staff.ToString())
            {
                var userID = _jwt.DecodeJwtUserId(token);
                schedules = await _scheduleUserRepo.FindAsync(s => s.AssignToId == userID, int.MaxValue);
            }
            res.Total = schedules.Count();
            res.Assigned = schedules.Count(s => s.Status == ScheduleUserStatusEnum.Assigned.ToString());
            res.Pending = schedules.Count(s => s.Status == ScheduleUserStatusEnum.Pending.ToString());
            res.Approved = schedules.Count(s => s.Status == ScheduleUserStatusEnum.Approved.ToString());
            res.Expired = schedules.Count(s => s.Status == ScheduleUserStatusEnum.Expired.ToString());
            res.Rejected = schedules.Count(s => s.Status == ScheduleUserStatusEnum.Rejected.ToString());
            res.Cancel = schedules.Count(s => s.Status == ScheduleUserStatusEnum.Cancel.ToString());


            return res;
        }

        public async Task<Result<DashboardSchedule>> GetSchedule()
        {
            var res = new DashboardSchedule()
            {
                Month = 0,
                Week = 0
            };
            var schedules = (await _scheduleRepo.GetAllAsync());
            res.Month = schedules.Count(s => s.ScheduleTypeId == (int)ScheduleTypeEnum.ProcessMonth);
            res.Week = schedules.Count(s => s.ScheduleTypeId == (int)ScheduleTypeEnum.ProcessWeek);

            return res;
        }

        public async Task<Result<DashboardUserRes>> GetUser()
        {
            var res = new DashboardUserRes()
            {
                Admin = 0,
                DepartmentManager = 0,
                Manager = 0,
                Security = 0,
                Staff = 0,
            };
            var user = (await _userRepo.GetAllAsync());
            res.Admin = user.Count(s => s.RoleId == (int)UserRoleEnum.Admin);
            res.Manager = user.Count(s => s.RoleId == (int)UserRoleEnum.Manager);
            res.DepartmentManager = user.Count(s => s.RoleId == (int)UserRoleEnum.DepartmentManager);
            res.Security = user.Count(s => s.RoleId == (int)UserRoleEnum.Security);
            res.Staff = user.Count(s => s.RoleId == (int)UserRoleEnum.Staff);
            return res;
        }

        public async Task<Result<DashBoardVisitRes>> GetVisit(string token)
        {
            var res = new DashBoardVisitRes()
            {
                Total = 0,
                Daily = 0,
                Month = 0,
                Week = 0,
                Cancel = 0,
                Violation = 0,
                Active = 0,
                ActiveTemporary = 0,
                Pending = 0,
                Inactive = 0,
                ViolationResolved = 0,
            };
            var role = _jwt.DecodeJwt(token);
            var visit = new List<Visit>();
            if (role == UserRoleEnum.Admin.ToString() || role == UserRoleEnum.Manager.ToString())
            {
                visit = (await _visitRepo.FindAsync(s => true, int.MaxValue, includeProperties: "ScheduleUser.Schedule")).ToList();
            }
            else if (role == UserRoleEnum.DepartmentManager.ToString())
            {
                var userID = _jwt.DecodeJwtUserId(token);
                var dm = (await _userRepo.FindAsync(s => s.UserId == userID)).FirstOrDefault();
                visit = (await _visitRepo.FindAsync(s => s.ResponsiblePerson.DepartmentId == dm.DepartmentId, int.MaxValue, includeProperties: "ScheduleUser.Schedule")).ToList();
            }
            else if (role == UserRoleEnum.Staff.ToString())
            {
                var userID = _jwt.DecodeJwtUserId(token);
                visit = (await _visitRepo.FindAsync(s => s.ResponsiblePersonId == userID, int.MaxValue, includeProperties: "ScheduleUser.Schedule")).ToList();
            }

            res.Total = visit.Count();
            res.Daily = visit.Count(s => s.ScheduleUser == null);
            res.Week = visit.Count(s => s.ScheduleUser != null && s.ScheduleUser.Schedule.ScheduleTypeId == (int)ScheduleTypeEnum.ProcessWeek);
            res.Month = visit.Count(s => s.ScheduleUser != null && s.ScheduleUser.Schedule.ScheduleTypeId == (int)ScheduleTypeEnum.ProcessMonth);
            res.Cancel = visit.Count(s => s.VisitStatus == VisitStatusEnum.Cancelled.ToString());
            res.Violation = visit.Count(s => s.VisitStatus == VisitStatusEnum.Violation.ToString());
            res.Active = visit.Count(s => s.VisitStatus == VisitStatusEnum.Active.ToString());
            res.Inactive = visit.Count(s => s.VisitStatus == VisitStatusEnum.Inactive.ToString());
            res.Pending = visit.Count(s => s.VisitStatus == VisitStatusEnum.Pending.ToString());
            res.ActiveTemporary = visit.Count(s => s.VisitStatus == VisitStatusEnum.ActiveTemporary.ToString());
            res.ViolationResolved = visit.Count(s => s.VisitStatus == VisitStatusEnum.ViolationResolved.ToString());

            return res;
        }

        public async Task<Result<DashboardVisitorRes>> GetVisitor()
        {
            var res = new DashboardVisitorRes()
            {
                Active = 0,
                Total = 0,
                Inavtive = 0,
            };
            var visitor = (await _visitorRepo.GetAllAsync());
            res.Total = visitor.Count();
            res.Active = visitor.Count(s => s.Status == VisitorStatusEnum.Active.ToString());
            res.Inavtive = visitor.Count(s => s.Status == VisitorStatusEnum.InActive.ToString());
            return res;
        }

        public async Task<Result<VisitorSessionCountMonthRes>> GetVisitorSessionCountByMonth(int year, int month, string token)
        {
            var userAuthor = _jwt.DecodeAuthorJwt(token);
            var res = new VisitorSessionCountMonthRes();
            var visitorSessions = await _visitorSessionRepo.GetAllAsync();
            if (userAuthor.Role == UserRoleEnum.Admin.ToString() || userAuthor.Role == UserRoleEnum.Manager.ToString())
            {
                visitorSessions = (await _visitorSessionRepo.GetAllAsync());
            }
            else if (userAuthor.Role == UserRoleEnum.DepartmentManager.ToString())
            {
                var userID = _jwt.DecodeJwtUserId(token);
                var dm = (await _userRepo.FindAsync(s => s.UserId == userID)).FirstOrDefault();
                visitorSessions = await _visitorSessionRepo.FindAsync(s => s.VisitDetail.Visit.CreateBy.DepartmentId == userAuthor.DepartmentId, int.MaxValue);
            }
            else if (userAuthor.Role == UserRoleEnum.Staff.ToString())
            {
                var userID = _jwt.DecodeJwtUserId(token);
                visitorSessions = await _visitorSessionRepo.FindAsync(s => s.VisitDetail.Visit.ResponsiblePersonId == userAuthor.UserId, int.MaxValue);
            }
            res.DailyCounts = visitorSessions
                .Where(vs => vs.CheckinTime.Year == year && vs.CheckinTime.Month == month)
                .GroupBy(vs => vs.CheckinTime.Day)
                .Select(g => new DailyCount
                {
                    Day = g.Key,
                    Count = g.Count()
                })
                .OrderBy(dc => dc.Day)
                .ToList();

            return Result.Success(res);
        }

        public async Task<Result<VisitorSessionCountRes>> GetVisitorSessionCountByYear(int year, string token)
        {
            var userAuthor = _jwt.DecodeAuthorJwt(token);

            var res = new VisitorSessionCountRes();
            var visitorSessions = await _visitorSessionRepo.GetAllAsync();
            if (userAuthor.Role == UserRoleEnum.Admin.ToString() || userAuthor.Role == UserRoleEnum.Manager.ToString())
            {
                visitorSessions = (await _visitorSessionRepo.GetAllAsync());
            }
            else if (userAuthor.Role == UserRoleEnum.DepartmentManager.ToString())
            {
                var userID = _jwt.DecodeJwtUserId(token);
                var dm = (await _userRepo.FindAsync(s => s.UserId == userID)).FirstOrDefault();
                visitorSessions = await _visitorSessionRepo.FindAsync(s => s.VisitDetail.Visit.CreateBy.DepartmentId == userAuthor.DepartmentId, int.MaxValue);
            }
            else if (userAuthor.Role == UserRoleEnum.Staff.ToString())
            {
                var userID = _jwt.DecodeJwtUserId(token);
                visitorSessions = await _visitorSessionRepo.FindAsync(s => s.VisitDetail.Visit.ResponsiblePersonId == userAuthor.UserId, int.MaxValue);
            }

            res.MonthlyCounts = visitorSessions
                .Where(vs => vs.CheckinTime.Year == year)
                .GroupBy(vs => vs.CheckinTime.Month)
                .Select(g => new MonthlyCount
                {
                    Month = g.Key,
                    Count = g.Count()
                })
                .OrderBy(mc => mc.Month)
                .ToList();

            return Result.Success(res);
        }

        public async Task<Result<List<GetVisitorSessionRes>>> GetRecentVisitorSessions(string token, int count = 5)
        {
            var userAuthor = _jwt.DecodeAuthorJwt(token);

            var visitSession = (await _visitorSessionRepo.FindAsync(
                 s => true /*s.VisitDetail.Visit.CreateBy.DepartmentId == userAuthor.DepartmentId
                && s.CheckinTime.Date == DateTime.Now.Date*/,
                 count, 1,
                 orderBy: s => s.OrderByDescending(s => s.CheckinTime),
                 includeProperties: "SecurityIn,SecurityOut,GateIn,GateOut,VisitorSessionsImages,VisitDetail.Visitor"
             )).ToList();
            if (userAuthor.Role == UserRoleEnum.Admin.ToString() || userAuthor.Role == UserRoleEnum.Manager.ToString())
            {
                visitSession = (await _visitorSessionRepo.FindAsync(
                                     s => true /*s.VisitDetail.Visit.CreateBy.DepartmentId == userAuthor.DepartmentId
                                    && s.CheckinTime.Date == DateTime.Now.Date*/,
                                     count, 1,
                                     orderBy: s => s.OrderByDescending(s => s.CheckinTime),
                                     includeProperties: "SecurityIn,SecurityOut,GateIn,GateOut,VisitorSessionsImages,VisitDetail.Visitor"
                                 )).ToList();
            }
            else if (userAuthor.Role == UserRoleEnum.DepartmentManager.ToString())
            {
                visitSession = (await _visitorSessionRepo.FindAsync(
                                      s => s.VisitDetail.Visit.CreateBy.DepartmentId == userAuthor.DepartmentId /*s.VisitDetail.Visit.CreateBy.DepartmentId == userAuthor.DepartmentId
                                    && s.CheckinTime.Date == DateTime.Now.Date*/,
                                      count, 1,
                                      orderBy: s => s.OrderByDescending(s => s.CheckinTime),
                                      includeProperties: "SecurityIn,SecurityOut,GateIn,GateOut,VisitorSessionsImages,VisitDetail.Visitor"
                                  )).ToList();
            }
            else if (userAuthor.Role == UserRoleEnum.Staff.ToString())
            {
                visitSession = (await _visitorSessionRepo.FindAsync(
                                    s => s.VisitDetail.Visit.ResponsiblePersonId == userAuthor.UserId /*s.VisitDetail.Visit.CreateBy.DepartmentId == userAuthor.DepartmentId
                                    && s.CheckinTime.Date == DateTime.Now.Date*/,
                                    count, 1,
                                    orderBy: s => s.OrderByDescending(s => s.CheckinTime),
                                    includeProperties: "SecurityIn,SecurityOut,GateIn,GateOut,VisitorSessionsImages,VisitDetail.Visitor"
                                )).ToList();
            }
            var result = _mapper.Map<List<GetVisitorSessionRes>>(visitSession);
            return Result.Success(result);
        }
        public async Task<Result<List<VisitorSessionStatusCountRes>>> GetVisitorSessionCountByStatusForToday(string token)
        {
            var userAuthor = _jwt.DecodeAuthorJwt(token);

            var today = DateTime.Today;
            var visitorSessions = await _visitorSessionRepo.FindAsync(
                vs => vs.CheckinTime.Date == today,
                int.MaxValue, 1,
                orderBy: vs => vs.OrderBy(v => v.Status),
                includeProperties: "SecurityIn,SecurityOut,GateIn,GateOut,VisitorSessionsImages"
            );
            if (userAuthor.Role == UserRoleEnum.Admin.ToString() || userAuthor.Role == UserRoleEnum.Manager.ToString())
            {
                visitorSessions = (await _visitorSessionRepo.FindAsync(
                                     s => true /*s.VisitDetail.Visit.CreateBy.DepartmentId == userAuthor.DepartmentId
                                    && s.CheckinTime.Date == DateTime.Now.Date*/,
                                     int.MaxValue, 1,
                                     orderBy: s => s.OrderByDescending(s => s.CheckinTime),
                                     includeProperties: "SecurityIn,SecurityOut,GateIn,GateOut,VisitorSessionsImages,VisitDetail.Visitor"
                                 )).ToList();
            }
            else if (userAuthor.Role == UserRoleEnum.DepartmentManager.ToString())
            {
                visitorSessions = (await _visitorSessionRepo.FindAsync(
                                      s => s.VisitDetail.Visit.CreateBy.DepartmentId == userAuthor.DepartmentId /*s.VisitDetail.Visit.CreateBy.DepartmentId == userAuthor.DepartmentId
                                    && s.CheckinTime.Date == DateTime.Now.Date*/,
                                      int.MaxValue, 1,
                                      orderBy: s => s.OrderByDescending(s => s.CheckinTime),
                                      includeProperties: "SecurityIn,SecurityOut,GateIn,GateOut,VisitorSessionsImages,VisitDetail.Visitor"
                                  )).ToList();
            }
            else if (userAuthor.Role == UserRoleEnum.Staff.ToString())
            {
                visitorSessions = (await _visitorSessionRepo.FindAsync(
                                    s => s.VisitDetail.Visit.ResponsiblePersonId == userAuthor.UserId /*s.VisitDetail.Visit.CreateBy.DepartmentId == userAuthor.DepartmentId
                                    && s.CheckinTime.Date == DateTime.Now.Date*/,
                                    int.MaxValue, 1,
                                    orderBy: s => s.OrderByDescending(s => s.CheckinTime),
                                    includeProperties: "SecurityIn,SecurityOut,GateIn,GateOut,VisitorSessionsImages,VisitDetail.Visitor"
                                )).ToList();
            }
            var statusCounts = visitorSessions
                .GroupBy(vs => vs.Status)
                .Select(g => new VisitorSessionStatusCountRes
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .OrderBy(sc => sc.Status)
                .ToList();
            if (!statusCounts.Any(s => s.Status == SessionStatus.CheckOut.ToString()))
            {
                statusCounts.Add(new VisitorSessionStatusCountRes
                {
                    Status = SessionStatus.CheckOut.ToString(),
                    Count = 0
                });
            }
            if (!statusCounts.Any(s => s.Status == SessionStatus.CheckIn.ToString()))
            {
                statusCounts.Add(new VisitorSessionStatusCountRes
                {
                    Status = SessionStatus.CheckIn.ToString(),
                    Count = 0
                });
            }

            return Result.Success(statusCounts);
        }
        public async Task<Result<List<CardStatusCountRes>>> GetCardCountByStatus()
        {
            var cards = await _cardRepo.GetAllAsync();

            var statusCounts = cards
                .GroupBy(c => c.CardStatus)
                .Select(g => new CardStatusCountRes
                {
                    Status = g.Key.ToString(),
                    Count = g.Count()
                })
                .OrderBy(sc => sc.Status)
                .ToList();

            return Result.Success(statusCounts);
        }

        public async Task<Result<GetCardCountIssueRes>> GetCardBountByIssue()
        {
            var cardAll = await _cardRepo.GetAllAsync();
            var cards = await _cardRepo.FindAsync(c => c.VisitCards.Any(s => s.VisitCardStatus == VisitCardStatusEnum.Issue.ToString()), int.MaxValue, 1);
            var res = new GetCardCountIssueRes()
            {

                TotalCard = cardAll.Count(),
                TotalCardIssue = cards.Count()
            };
            return Result.Success(res);
        }
    }
}
