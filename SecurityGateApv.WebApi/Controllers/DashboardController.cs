using Microsoft.AspNetCore.Mvc;
using SecurityGateApv.Application.Services.Interface;

namespace SecurityGateApv.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        [HttpGet("Visit")]
        public async Task<IActionResult> GetVisit()
        {
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new Error("CreateUser", "Không có quyền truy cập"));
            }
            return Ok((await _dashboardService.GetVisit(token)).Value);
        }
        [HttpGet("User")]
        public async Task<IActionResult> GetUser()
        {
            return Ok((await _dashboardService.GetUser()).Value);
        }
        [HttpGet("Visitor")]
        public async Task<IActionResult> GetVisitor()
        {
            return Ok((await _dashboardService.GetVisitor()).Value);
        }
        [HttpGet("Schedule")]
        public async Task<IActionResult> GetSchedule()
        {
            return Ok((await _dashboardService.GetSchedule()).Value);
        }
        [HttpGet("ScheduleUser")]
        public async Task<IActionResult> GetScheduleUser(int? staffId)
        {
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new Error("CreateUser", "Không có quyền truy cập"));
            }
            var result = await _dashboardService.GetMission(token);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("VisitorSessionYear")]
        public async Task<IActionResult> GetVisitorSessionCountByYear(int year)
        {
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new Error("Authorization", "Yêu cầu quyền truy cập."));
            }
            var result = await _dashboardService.GetVisitorSessionCountByYear(year, token);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("VisitorSessionMonth")]
        public async Task<IActionResult> GetVisitorSessionCountByMonth(int year, int month)
        {
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new Error("Authorization", "Yêu cầu quyền truy cập."));
            }
            var result = await _dashboardService.GetVisitorSessionCountByMonth(year, month, token);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("RecentVisitorSessions")]
        public async Task<IActionResult> GetRecentVisitorSessions([FromQuery] int count = 5)
        {
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new Error("Authorization", "Yêu cầu quyền truy cập."));
            }
            var result = await _dashboardService.GetRecentVisitorSessions(token,count);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpGet("VisitorSessionStatusToday")]
        public async Task<IActionResult> GetVisitorSessionCountByStatusForToday()
        {
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new Error("Authorization", "Yêu cầu quyền truy cập."));
            }
            var result = await _dashboardService.GetVisitorSessionCountByStatusForToday(token);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
        [HttpGet("CardStatusCount")]
        public async Task<IActionResult> GetCardCountByStatus()
        {
            var result = await _dashboardService.GetCardCountByStatus();
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        } 
        [HttpGet("GetCardCountByIssue")]
        public async Task<IActionResult> GetCardCountByIssue()
        {
            var result = await _dashboardService.GetCardBountByIssue();
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }
    }
}
