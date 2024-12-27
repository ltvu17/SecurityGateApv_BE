using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class GetVisitRes
    {
        public int VisitId { get; set; }
        public string VisitName { get; private set; }
        public int VisitQuantity { get; private set; }
        public DateTime ExpectedStartTime { get; private set; }
        public DateTime ExpectedEndTime { get; private set; }
        public DateTime CreateTime { get; private set; }
        public DateTime UpdateTime { get; private set; }
        public string? Description { get; private set; }
        public string VisitStatus { get; private set; }
        public CreateByRes CreateBy { get; private set; }

        public CreateByRes? UpdateBy { get; private set; }
        public ScheduleUserRes? ScheduleUser { get; set; }

        //public ScheduleResForVisit Schedule { get; private set; }
        public ICollection<VisitDetailRes> VisitDetail { get; set; }
        //public ICollection<VisitProcessRes> VisitProcess { get;  set; }
        public int VisitorSessionCount { get; set; }

    }
    public class GetVisitNoDetailRes
    {
        public int VisitId { get; set; }
        public string VisitName { get; private set; }
        public int VisitQuantity { get; private set; }
        public DateTime ExpectedStartTime { get; private set; }
        public DateTime ExpectedEndTime { get; private set; }
        public DateTime CreateTime { get; private set; }
        public DateTime UpdateTime { get; private set; }
        public string? Description { get; private set; }
        public string VisitStatus { get; private set; }
        public ScheduleUserRes? ScheduleUser { get; set; }

        public CreateByRes CreateBy { get; private set; }

        public CreateByRes? UpdateBy { get; private set; }
        public CreateByRes ResponsiblePerson { get; private set; }
        public int VisitorSessionCount { get; set; }


        //public ScheduleResForVisit? Schedule { get; private set; }
    }
    public class VisitDetailRes
    {
        public int VisitDetailId { get; set; }
        public TimeSpan ExpectedStartHour { get; private set; }
        public TimeSpan ExpectedEndHour { get; private set; }
        public bool Status { get; private set; }
        public VisitorDetailRes Visitor { get; set; }
        public int VisitorSessionCurrentDay { get; set; } = 0;
    }
    public class VisitorDetailRes
    {
        public int VisitorId { get; set; }
        public string VisitorName { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string CredentialsCard { get; set; }
    }
    public class GetVisitByDateRes
    {

        public int VisitId { get; set; }
        public string VisitName { get; set; }
        public int VisitQuantity { get; set; }
        public string? Description { get; set; }
        public string CreateByname { get; set; }
        public string ScheduleTypeName { get; set; }
        public int VisitorNoSessionCount { get; set; }
        public int VisitorCheckkInCount { get; set; }
        public int VisitorCheckkOutCount { get; set; }
        //public int VisitorSessionCheckedOutCount { get; set; }
        //public int VisitorSessionCheckedInCount { get; set; }
        //public int VisitorCheckOutedCount { get; set; }
        public TimeSpan? VisitDetailStartTime { get; set; }
        public TimeSpan? VisitDetailEndTime { get; set; }
        public string VisitStatus { get; set; }

    }


    public class CreateByRes
    {
        public string FullName { get; set; }
    }

    public class VisitProcessRes
    {
        public string DaysOfProcess { get; set; }
    }
    public class ScheduleResForVisit
    {
        public int ScheduleId { get; private set; }
        public string ScheduleName { get; private set; }
        public GetScheduleTypeRes ScheduleType { get; set; }
    }
}
