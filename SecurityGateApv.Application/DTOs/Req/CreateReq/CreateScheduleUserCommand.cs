using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req.CreateReq
{
    public class CreateScheduleUserCommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Note { get; set; }
        [JsonPropertyName("deadlineTime")]
        public DateTime DeadlineTime { get; set; }
        public int ScheduleId { get; set; }
        public int AssignToId { get; set; }
        public int? MaxPersonQuantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
