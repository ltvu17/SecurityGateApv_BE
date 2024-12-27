using SecurityGateApv.Domain.Errors;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Models
{
    public class ScheduleType
    {

        [Key]
        public int ScheduleTypeId { get; private set; }
        public string ScheduleTypeName { get; private set; }
        public string Description { get; private set; }
        public bool Status { get; private set; }
        public ICollection<Schedule> VisitTypes { get; private set; }
        public ScheduleType()
        {
            
        }
        public ScheduleType(int scheduleTypeId, string scheduleTypeName, string description, bool status)
        {
            ScheduleTypeId = scheduleTypeId;
            ScheduleTypeName = scheduleTypeName;
            Description = description;
            Status = status;
        }

        //Create function to create schedule type using constructor and retun result<ScheduleType>
        public static Result<ScheduleType> Create(int scheduleTypeId, string scheduleTypeName, string description, bool status)
        {
            if (string.IsNullOrEmpty(scheduleTypeName))
            {
                return  Result.Failure<ScheduleType>(Error.ScheduleTypeInputValid);
            }
            if (string.IsNullOrEmpty(description))
            {
                return Result.Failure<ScheduleType>(Error.ScheduleTypeInputValid);
            }
            var scheduleType = new ScheduleType(scheduleTypeId, scheduleTypeName, description, status);

            return scheduleType;
        }
    }
}
