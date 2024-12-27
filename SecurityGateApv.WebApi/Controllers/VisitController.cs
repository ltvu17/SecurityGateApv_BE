using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityGateApv.Application.DTOs.Req;
using SecurityGateApv.Application.DTOs.Req.UpdateReq;
using SecurityGateApv.Application.Services;
using SecurityGateApv.Application.Services.Interface;

namespace SecurityGateApv.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitController : Controller
    {
        private readonly IVisitService _visitService;
        public VisitController(IVisitService visitService)
        {
            _visitService = visitService;
        }
        //[Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public async Task<ActionResult> GetAllVisits([FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            //var token = Request.Headers["Authorization"];
            //if (string.IsNullOrEmpty(token))
            //{
            //    return BadRequest(new Error("GetVisitByStatus", "Invalid Token"));
            //}
            if (pageNumber == -1 || pageSize == -1)
            {
                var resultAll = await _visitService.GetAllVisit(int.MaxValue, 1);
                if (resultAll.IsFailure)
                {
                    return BadRequest(resultAll.Error);
                }
                return Ok(resultAll.Value);
            }
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }
            var result = await _visitService.GetAllVisit(pageSize, pageNumber);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("{visitId}")]
        public async Task<ActionResult> GetVisitDetailByVisitId(int visitId)
        {
            var result = await _visitService.GetVisitDetailByVisitId(visitId);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        //[HttpGet("VisitDetailId/{visitDetailId}")]
        //public async Task<ActionResult> GetVisitByVisiDetailtId(int visitDetailId)
        //{
        //    var result = await _visitService.GetVisitByVisiDetailtId(visitDetailId);

        //    if (result.IsFailure)
        //    {
        //        return BadRequest(result.Error);
        //    }
        //    return Ok(result.Value);
        //} 
        [HttpGet("ScheduleUserId/{scheduleUserId}")]
        public async Task<ActionResult> GetVisitByScheduleUserId(int scheduleUserId)
        {
            var result = await _visitService.GetVisitByScheduleUserId(scheduleUserId);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("Status")]
        public async Task<ActionResult> GetVisitDetailByStatus([FromQuery] string? status, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new Error("GetVisitByStatus", "Invalid Token"));
            }
            if (pageNumber == -1 || pageSize == -1)
            {
                var resultAll = await _visitService.GetVisitDetailByStatus(token, status, 1, int.MaxValue);
                if (resultAll.IsFailure)
                {
                    return BadRequest(resultAll.Error);
                }
                return Ok(resultAll.Value);
            }
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }
            var result = await _visitService.GetVisitDetailByStatus(token, status, pageNumber, pageSize);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("CreateBy/{createById}")]
        public async Task<ActionResult> GetVisitDetailByCreateById(int createById, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            if (pageNumber == -1 || pageSize == -1)
            {
                var resultAll = await _visitService.GetVisitDetailByCreateById(createById, 1, int.MaxValue);
                if (resultAll.IsFailure)
                {
                    return BadRequest(resultAll.Error);
                }
                return Ok(resultAll.Value);
            }
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }
            var result = await _visitService.GetVisitDetailByCreateById(createById, pageNumber, pageSize);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("ResponePerson/{responePersonId}")]
        public async Task<ActionResult> GetVisitDetailByResponePersonId(int responePersonId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            if (pageNumber == -1 || pageSize == -1)
            {
                var resultAll = await _visitService.GetVisitDetailByResponePersonId(responePersonId, 1, int.MaxValue);
                if (resultAll.IsFailure)
                {
                    return BadRequest(resultAll.Error);
                }
                return Ok(resultAll.Value);
            }
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }
            var result = await _visitService.GetVisitDetailByResponePersonId(responePersonId, pageNumber, pageSize);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("DepartmentId/{departmentId}")]
        public async Task<ActionResult> GetVisitDetailByDepartmentIdId(int departmentId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            if (pageNumber == -1 || pageSize == -1)
            {
                var resultAll = await _visitService.GetVisitByDepartmentId(departmentId, 1, int.MaxValue);
                if (resultAll.IsFailure)
                {
                    return BadRequest(resultAll.Error);
                }
                return Ok(resultAll.Value);
            }
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }
            var result = await _visitService.GetVisitByDepartmentId(departmentId, pageNumber, pageSize);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("UserId/{userId}")]
        public async Task<ActionResult> GetVisitByUserId(int userId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            if (pageNumber == -1 || pageSize == -1)
            {
                var resultAll = await _visitService.GetVisitByUserId(userId, 1, int.MaxValue);
                if (resultAll.IsFailure)
                {
                    return BadRequest(resultAll.Error);
                }
                return Ok(resultAll.Value);
            }
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }
            var result = await _visitService.GetVisitByUserId(userId, pageNumber, pageSize);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("Day")]
        public async Task<ActionResult> GetVisitByDate([FromQuery] int pageSize, [FromQuery] int pageNumber, [FromQuery] DateTime date)
        {
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new Error("CreateUser", "Invalid Token"));
            }
            if (pageNumber == -1 || pageSize == -1)
            {
                var resultAll = await _visitService.GetVisitByDate(int.MaxValue, 1, date, token);
                if (resultAll.IsFailure)
                {
                    return BadRequest(resultAll.Error);
                }
                return Ok(resultAll.Value);
            }
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }
            var result = await _visitService.GetVisitByDate(pageSize, pageNumber, date,token);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("Day/{visitId}")]
        public async Task<ActionResult> GetAllVisitsByDate([FromQuery] int pageSize, [FromQuery] int pageNumber, int visitId)
        {
            if (pageNumber == -1 || pageSize == -1)
            {
                var resultAll = await _visitService.GetVisitByDateByVisitID(int.MaxValue, 1, visitId);
                if (resultAll.IsFailure)
                {
                    return BadRequest(resultAll.Error);
                }
                return Ok(resultAll.Value);
            }
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }
            var result = await _visitService.GetVisitByDateByVisitID(pageSize, pageNumber, visitId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("VisitDetail/{visitId}")]
        public async Task<ActionResult> GetVisitDetailByVisitId(int visitId, int pageNumber, int pageSize)
        {
            var result = await _visitService.GetVisitDetailByVisitId(visitId, pageNumber, pageSize);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("Day/VerifiedId/{verifiedId}")]
        public async Task<ActionResult> GetVisitByCurrentDateAndCredentialCard(string verifiedType, string verifiedId, [FromQuery] DateTime date)
        {
            var result = await _visitService.GetVisitByCurrentDateAndCredentialCard(verifiedType, verifiedId, date);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
        [HttpGet("Day/CardVerified/{cardVerified}")]
        public async Task<ActionResult> GetVisitByDayAndCardVerified(string cardVerified, [FromQuery] DateTime date)
        {
            var result = await _visitService.GetVisitByDayAndCardVerified(cardVerified, date);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
        [HttpPut("Status/{visitId}")]
        public async Task<ActionResult> ReportVisit(int visitId, string action)
        {
            if (action == "Violation")
            {
                var result = await _visitService.ReportVisit(visitId);
                if (result.IsFailure)
                {
                    return BadRequest(result.Error);
                }
                return Ok(result.Value);
            }
            if (action == "Cancelled")
            {
                var result = await _visitService.CancelVisit(visitId);
                if (result.IsFailure)
                {
                    return BadRequest(result.Error);
                }
                return Ok(result.Value);
            }
            if (action == "Active")
            {
                var result = await _visitService.ActiveVisit(visitId);
                if (result.IsFailure)
                {
                    return BadRequest(result.Error);
                }
                return Ok(result.Value);
            }
            if (action == "ViolationResolved")
            {
                var result = await _visitService.ViolationResolvedVisit(visitId);
                if (result.IsFailure)
                {
                    return BadRequest(result.Error);
                }
                return Ok(result.Value);
            }
            return BadRequest(new Domain.Errors.Error("Visit.ReportVisit", "Action must be \"Violation | Cancelled | Active | ViolationResolved\""));
        }
        [HttpPost]
        public async Task<ActionResult> CreateVisit(VisitCreateCommand command)
        {
            foreach (var x in command.VisitDetail)
            {
                if (x.ExpectedEndHour > TimeSpan.Parse("20:00:00"))
                {
                    return StatusCode(410, "Thời gian không hợp lệ");
                }
                if (x.ExpectedStartHour < TimeSpan.Parse("07:00:00"))
                {
                    return StatusCode(410, "Thời gian không hợp lệ");
                }
            }
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new Error("CreateUser", "Invalid Token"));
            }
            var result = await _visitService.CreateVisit(command, token);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost("Daily")]
        public async Task<ActionResult> CreateVisitDaily(VisitCreateCommandDaily command)
        {
            foreach (var x in command.VisitDetail)
            {
                if (x.ExpectedEndHour > TimeSpan.Parse("20:00:00"))
                {
                    return StatusCode(410, "Thời gian không hợp lệ");
                }
                if (x.ExpectedStartHour < TimeSpan.Parse("07:00:00"))
                {
                    return StatusCode(410, "Thời gian không hợp lệ");
                }
            }
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new Error("CreateVisitDaily", "Invalid Token"));
            }
            var result = await _visitService.CreateVisitDaily(command, token);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        //[HttpPut("{visitId}")]
        //public async Task<ActionResult> UpdateVisit(int visitId, VisitCreateCommand command)
        //{
        //    var result = await _visitService.UpdateVisit(visitId, command);

        //    if (result.IsFailure)
        //    {
        //        return BadRequest(result.Error);
        //    }
        //    return Ok(result.Value);
        //}        
        [HttpPut("BeforeStartDate/{visitId}")]
        public async Task<ActionResult> UpdateVisitBeforeStartDate(int visitId, UpdateVisitBeforeStartDateCommand command)
        {
            var result = await _visitService.UpdateVisitBeforeStartDate(visitId, command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("AfterStartDate/{visitId}")]
        public async Task<ActionResult> UpdateVisitAfterStartDate(int visitId, UpdateVisitAfterStartDateCommand command)
        {
            var result = await _visitService.UpdateVisitAfterStartDate(visitId, command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpDelete("{visitId}")]
        public async Task<ActionResult> DeleteVisit(int visitId)
        {
            var result = await _visitService.DeleteVisit(visitId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

    }
}
