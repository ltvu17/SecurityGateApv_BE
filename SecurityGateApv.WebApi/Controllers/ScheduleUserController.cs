using Microsoft.AspNetCore.Mvc;
using SecurityGateApv.Application.DTOs.Req.CreateReq;
using SecurityGateApv.Application.Services;
using SecurityGateApv.Application.Services.Interface;

namespace SecurityGateApv.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleUserController : Controller
    {
        private readonly IScheduleUserService _scheduleUserService;

        public ScheduleUserController(IScheduleUserService scheduleUserService)
        {
            _scheduleUserService = scheduleUserService;
        }
        //[HttpGet()]
        //public async Task<IActionResult> GetAllSchedule(int pageNumber, int pageSize)
        //{
        //    var result = await _scheduleUserService.GetAllSchedule(pageNumber, pageSize);
        //    if (result.IsFailure)
        //    {
        //        return BadRequest(result.Error);
        //    }

        //    return Ok(result.Value);
        //}
        [HttpGet("UserId/{userId}")]
        public async Task<IActionResult> GetScheduleUserByUserId(int userId, int pageNumber, int pageSize)
        {
            if(pageSize == -1 || pageNumber == -1)
            {
                var resultALL = await _scheduleUserService.GetScheduleUserByUserId(userId, 1, int.MaxValue);
                if (resultALL.IsFailure)
                {
                    return BadRequest(resultALL.Error);
                }

                return Ok(resultALL.Value);
            }
            var result = await _scheduleUserService.GetScheduleUserByUserId(userId, pageNumber, pageSize);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
        [HttpGet("Staff/{staffId}")]
        public async Task<IActionResult> GetScheduleUserByStaffId(int staffId)
        {
            var result = await _scheduleUserService.GetScheduleUserByStaffId(staffId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
        [HttpGet("{scheduleUserId}")]
        public async Task<IActionResult> GetScheduleUserById(int scheduleUserId)
        {
            var result = await _scheduleUserService.GetScheduleUserById(scheduleUserId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        } 
        [HttpGet("Status/{status}/UserId/{userId}")]
        public async Task<IActionResult> GetScheduleUserByUserIdAndStatus(int userId,string status, int pageNumber, int pageSize)
        {
            var result = await _scheduleUserService.GetScheduleUserByUserIdAndStatus(userId, status, pageNumber, pageSize);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
        [HttpPost()]
        public async Task<IActionResult> CreateScheduleUser([FromBody] CreateScheduleUserCommand request)
        {
            var result = await _scheduleUserService.CreateScheduleUser(request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("Reject/{scheduleUserId}")]
        public async Task<IActionResult> RejectScheduleUser(int scheduleUserId)
        {
            var result = await _scheduleUserService.RejectScheduleUser(scheduleUserId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("Cancel/{scheduleUserId}")]
        public async Task<IActionResult> CancelScheduleUser(int scheduleUserId)
        {
            var result = await _scheduleUserService.CancelScheduleUser(scheduleUserId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("Approve/{scheduleUserId}")]
        public async Task<IActionResult> AproveScheduleUser(int scheduleUserId)
        {
            var result = await _scheduleUserService.AproveScheduleUser(scheduleUserId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
