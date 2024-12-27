using SecurityGateApv.Domain.Common;
using SecurityGateApv.Domain.Enums;
using SecurityGateApv.Domain.Errors;
using SecurityGateApv.Domain.Interfaces.DomainDTOs;
using SecurityGateApv.Domain.Interfaces.EmailSender;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Models
{
    public class Visit
    {
        public Visit()
        {
            
        }

        public Visit(string visitName, int visitQuantity, DateTime expectedStartTime, DateTime expectedEndTime, DateTime createTime
     , DateTime updateTime, string? description, string visitStatus, int createById, int responsiblePersonId, int? scheduleUserId)
        {
            VisitName = visitName;
            VisitQuantity = visitQuantity;
            ExpectedStartTime = expectedStartTime;
            ExpectedEndTime = expectedEndTime;
            CreateTime = createTime;
            UpdateTime = updateTime;
            Description = description;
            VisitStatus = visitStatus;
            CreateById = createById;
            ScheduleUserId = scheduleUserId;
            ResponsiblePersonId = responsiblePersonId;
        }

        [Key]
        public int VisitId { get; set; }
        public string VisitName { get; set; }
        public int VisitQuantity { get; set; }
        public DateTime ExpectedStartTime { get; set; }
        public DateTime ExpectedEndTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string? Description { get; set; }
        public string VisitStatus { get; set; }

        [ForeignKey("CreateBy")]
        public int CreateById { get; set; }
        public User CreateBy { get; set; }

        [ForeignKey("UpdateBy")]
        public int? UpdateById { get; set; }
        public User? UpdateBy { get;  set; }

        [ForeignKey("ScheduleUser")]
        public int? ScheduleUserId { get;  set; }
        public ScheduleUser? ScheduleUser { get;  set; }

        [ForeignKey("ResponsiblePerson")]
        public int? ResponsiblePersonId { get; set; }
        public User ResponsiblePerson { get; set; }

        public ICollection<VisitDetail> VisitDetail { get; set; } = new List<VisitDetail>();

        public static Result<Visit> Create(string visitName, int visitQuantity, DateTime expectedStartTime, DateTime expectedEndTime, DateTime createTime
            , DateTime updateTime, string? description, string visitStatus, int createById, int responsiblePersonId, int? scheduleUserId)
        {
            var result = new Visit(visitName, visitQuantity, expectedStartTime, expectedEndTime, createTime
            , updateTime, description, visitStatus, createById, responsiblePersonId, scheduleUserId);
            return result;
        }

        public async Task<Result<Visit>> AddVisitDetailOfOldVisitor(IEnumerable<VisitDetail> visitSchedule,ScheduleUser? scheduleUser, Schedule schedule, TimeSpan expectedStartHour, TimeSpan expectedEndHour, bool status
            , int visitorId, string visitorName)
        {
            if (VisitDetail.Any(s => s.VisitorId == visitorId))
            {
                return Result.Failure<Visit>(Error.DuplicateVisitorDetail);
            }
            var visitDetailAdd = new VisitDetail(expectedStartHour, expectedEndHour, status
            , this, visitorId);
            var visitBusyOfVisitor = new List<ValidateVisitDateDTO>();
            foreach (VisitDetail visit in visitSchedule)
            {
                visitBusyOfVisitor.AddRange(await CommonService.CaculateBusyDates(visit));
            }
            if (scheduleUser != null)
            {

                visitDetailAdd.Visit.ScheduleUser = scheduleUser;
                visitDetailAdd.Visit.ScheduleUser.Schedule = schedule;
            }
            var visitorFutureBusy = await CommonService.CaculateBusyDates(visitDetailAdd);
            if (visitorFutureBusy.Count() == 0)
            {
                return Result.Failure<Visit>(Error.NoValidDateForVisit);
            }
            var error = new Dictionary<int, string>();
            foreach (var dateOfBusy in visitorFutureBusy)
            {

                var check = visitBusyOfVisitor.Where(s => dateOfBusy.VisitDate.Year == s.VisitDate.Year && dateOfBusy.VisitDate.Month == s.VisitDate.Month && dateOfBusy.VisitDate.Day == s.VisitDate.Day);

                if (check != null)
                {
                    foreach (var day in check)
                    {
                        if ((day.TimeIn <= dateOfBusy.TimeIn && day.TimeOut >= dateOfBusy.TimeOut))
                        {
                            error.TryGetValue((int)day.VisitId, out string date);
                            if (date != null)
                            {
                                date += $", {day.VisitDate.ToShortDateString()}";
                                error.Remove((int)day.VisitId);
                                error.Add((int)day.VisitId, date);
                            }
                            else
                            {
                                error.Add((int)day.VisitId, day.VisitDate.ToShortDateString());
                            }
                            continue;
                        }
                        if ((day.TimeIn >= dateOfBusy.TimeIn && day.TimeIn < dateOfBusy.TimeOut))
                        {
                            error.TryGetValue((int)day.VisitId, out string date);
                            if (date != null)
                            {
                                date += $", {day.VisitDate.ToShortDateString()}";
                                error.Remove((int)day.VisitId);
                                error.Add((int)day.VisitId, date);
                            }
                            else
                            {
                                error.Add((int)day.VisitId, day.VisitDate.ToShortDateString());
                            }
                            continue;
                        }
                        if (day.TimeOut > dateOfBusy.TimeIn && day.TimeOut <= dateOfBusy.TimeOut)
                        {
                            error.TryGetValue((int)day.VisitId, out string date);
                            if (date != null)
                            {
                                date += $", {day.VisitDate.ToShortDateString()}";
                                error.Remove((int)day.VisitId);
                                error.Add((int)day.VisitId, date);
                            }
                            else
                            {
                                error.Add((int)day.VisitId, day.VisitDate.ToShortDateString());
                            }
                            continue;
                        }
                    }
                }
            }
            if (error.Distinct().Count() > 0)
            {
                return Result.Failure<Visit>(new Error("CreateVisit", "Khách " + visitorName + " bận ở cuộc Hẹn/Ngày: " + string.Join(", ", error.Values)));
            }
            VisitDetail.Add(visitDetailAdd);
            return this;
        }
        public async Task<Result<Visit>> CheckUpdateVisit(IEnumerable<VisitDetail> visitSchedule, ScheduleUser? scheduleUser, Schedule schedule, TimeSpan expectedStartHour, TimeSpan expectedEndHour, bool status
            , int visitorId)
        {
            var visitDetailAdd = new VisitDetail(expectedStartHour, expectedEndHour, status
            , this, visitorId);
            var visitBusyOfVisitor = new List<ValidateVisitDateDTO>();
            foreach (VisitDetail visit in visitSchedule)
            {
                visitBusyOfVisitor.AddRange(await CommonService.CaculateBusyDates(visit));
            }
            if (scheduleUser != null)
            {

                visitDetailAdd.Visit.ScheduleUser = scheduleUser;
                visitDetailAdd.Visit.ScheduleUser.Schedule = schedule;
            }
            var visitorFutureBusy = await CommonService.CaculateBusyDates(visitDetailAdd);
            if (visitorFutureBusy.Count() == 0)
            {
                return Result.Failure<Visit>(Error.NoValidDateForVisit);
            }
            var error = new Dictionary<int, string>();
            foreach (var dateOfBusy in visitorFutureBusy)
            {

                var check = visitBusyOfVisitor.Where(s => dateOfBusy.VisitDate.Year == s.VisitDate.Year && dateOfBusy.VisitDate.Month == s.VisitDate.Month && dateOfBusy.VisitDate.Day == s.VisitDate.Day);

                if (check != null)
                {
                    foreach (var day in check)
                    {
                        if (day.TimeIn >= dateOfBusy.TimeIn && day.TimeIn < dateOfBusy.TimeOut)
                        {
                            error.TryGetValue((int)day.VisitId, out string date);
                            if (date != null)
                            {
                                date += $", {day.VisitDate.ToShortDateString()}";
                                error.Remove((int)day.VisitId);
                                error.Add((int)day.VisitId, date);
                            }
                            else
                            {
                                error.Add((int)day.VisitId, day.VisitDate.ToShortDateString());
                            }
                            continue;
                        }
                        if (day.TimeOut > dateOfBusy.TimeIn && day.TimeOut <= dateOfBusy.TimeOut)
                        {
                            error.TryGetValue((int)day.VisitId, out string date);
                            if (date != null)
                            {
                                date += $", {day.VisitDate.ToShortDateString()}";
                                error.Remove((int)day.VisitId);
                                error.Add((int)day.VisitId, date);
                            }
                            else
                            {
                                error.Add((int)day.VisitId, day.VisitDate.ToShortDateString());
                            }
                            continue;
                        }
                    }
                }
            }
            if (error.Distinct().Count() > 0 && status == true)
            {
                return Result.Failure<Visit>(new Error("CreateVisit", "VisitorId " + visitorId + " busy at visit Id: " + string.Join(", ", error)));
            }
            if (VisitDetail.Any(s => s.VisitorId != visitorId))
            {
                VisitDetail.Add(visitDetailAdd);
            }
            else
            {
                var detail = this.VisitDetail.Where(s => s.VisitorId == visitorId).FirstOrDefault();
                if(detail == null)
                {
                    return Result.Failure<Visit>(new Error("Update", "Update Error"));
                }
                detail.ExpectedStartHour = expectedStartHour;
                detail.ExpectedEndHour = expectedEndHour;
                detail.Status = status;
            }
            return this;
        }
        public Result<Visit> Update(int updateById)
        {
            this.UpdateById = updateById;
            this.UpdateTime = DateTime.Now;
            return this;
        }
        public Result<Visit> UpdateAfterStartDate(List<VisitDetail> visitDetails)
        {
            this.VisitDetail = visitDetails;
            return this;
        }
        public Result<Visit> UpdateVisitAfterStartDate(int visitQuantity, DateTime expectedEndTime )
        {
            this.VisitQuantity = visitQuantity;
            this.ExpectedEndTime = expectedEndTime;
            return this;
        }
        public Result<Visit> UpdateStatusBackGroundWoker(string status)
        {
            this.VisitStatus = status;
            //this.UpdateTime = DateTime.Now;
            return this;
        }
        public Result<Visit> UpdateStatus(string status)
        {
            this.VisitStatus = status;
            //this.UpdateTime = DateTime.Now;
            return this;
        }
        public Result<Visit> RemoveDetail()
        {
            this.VisitDetail.Clear();
            return this;
        }
        public Result<Visit> AddEndTime(DateTime date)
        {
            this.ExpectedEndTime = date;
            return this;
        }
        public Result<Visit> AppendTimne(DateTime date)
        {
            this.ExpectedEndTime = date;
            return this;
        }
        public Result<Visit> ReportVisit()
        {
            this.VisitStatus = VisitStatusEnum.Violation.ToString();
            return this;
        }        
        public Result<Visit> CancelVisit()
        {
            this.VisitStatus = VisitStatusEnum.Cancelled.ToString();
            return this;
        }
        public Result<Visit> ActiveVisit()
        {
            this.VisitStatus = VisitStatusEnum.Active.ToString();
            return this;
        }
        public Result<Visit> ViolationResolvedVisit()
        {
            this.VisitStatus = VisitStatusEnum.ViolationResolved.ToString();
            return this;
        }
    }
}
