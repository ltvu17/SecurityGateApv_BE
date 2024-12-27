using Microsoft.AspNetCore.Mvc;
using SecurityGateApv.Application.Services;
using SecurityGateApv.Application.Services.Interface;

namespace SecurityGateApv.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleTypeController : Controller
    {
        private readonly IScheduleTypeService _scheduleTypeService;

        public ScheduleTypeController(IScheduleTypeService scheduleTypeService)
        {
            _scheduleTypeService = scheduleTypeService;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAllScheduleType()
        {
            var result = await _scheduleTypeService.GetAllScheduleType();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
