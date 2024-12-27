using Microsoft.AspNetCore.Mvc;
using SecurityGateApv.Application.DTOs.Req.CreateReq;
using SecurityGateApv.Application.DTOs.Req.UpdateReq;
using SecurityGateApv.Application.Services;
using SecurityGateApv.Application.Services.Interface;
using System.Drawing.Printing;

namespace SecurityGateApv.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAllSchedule(int pageNumber, int pageSize)
        {
            var result = await _scheduleService.GetAllSchedule( pageNumber,  pageSize);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("{schduleId}")]
        public async Task<IActionResult> GetScheduleById(int schduleId)
        {
            var result = await _scheduleService.GetScheduleById(schduleId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("Staff/AssignNotRead/{staffId}")]
        public async Task<IActionResult> GetScheduleNotReadOfStaffId(int staffId)
        {
            var result = await _scheduleService.GetScheduleNotReadByStaffId(staffId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("DepartmenManager/{departmenManagerId}")]
        public async Task<IActionResult> GetScheduleByDepartmenManagerId(int departmenManagerId, int pageNumber, int pageSize)
        {
            if (pageNumber == -1 || pageSize == -1)
            {
                var resultAll = await _scheduleService.GetScheduleByDepartmentManagerId(departmenManagerId, 1, int.MaxValue);
                return Ok(resultAll.Value);
            }
            var result = await _scheduleService.GetScheduleByDepartmentManagerId(departmenManagerId, pageNumber, pageSize);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleCommand request)
        {
            var result = await _scheduleService.CreateSchedule(request);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPut("{scheduleId}")]
        public async Task<IActionResult> UpdateSchedule([FromBody] UpdateScheduleCommand request,  int scheduleId)
        {
            var result = await _scheduleService.UpdateSchedule(request, scheduleId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpDelete("{scheduleId}")]
        public async Task<IActionResult> DeleteSchedule( int scheduleId)
        {
            var result = await _scheduleService.DeleteSchedule( scheduleId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
       
    }
}
