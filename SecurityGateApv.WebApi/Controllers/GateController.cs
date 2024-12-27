using Microsoft.AspNetCore.Mvc;
using SecurityGateApv.Application.DTOs.Req.CreateReq;
using SecurityGateApv.Application.DTOs.Req.UpdateReq;
using SecurityGateApv.Application.Services;
using SecurityGateApv.Application.Services.Interface;

namespace SecurityGateApv.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GateController : Controller
    {
        private readonly IGateService _gateService;
        public GateController(IGateService gateService)
        {
            _gateService = gateService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllGate()
        {
            var result = await _gateService.GetAllGate();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("{gateId}")]
        public async Task<ActionResult> GetGateById(int gateId)
        {
            var result = await _gateService.GetGateById(gateId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("CameraType")]
        public async Task<ActionResult> GetAllCameraType()
        {
            var result = await _gateService.GetAllCameraType();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("Camera/{gateId}")]
        public async Task<ActionResult> GetCamera(int gateId)
        {
            var result = await _gateService.GetCameraByGate(gateId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("GetAllGatePaging")]
        public async Task<ActionResult> GetAllGatePaging(int pageSize, int pageNumber)
        {
            var result = await _gateService.GetAllGatePaging(pageSize, pageNumber);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        } 
        [HttpPost()]
        public async Task<IActionResult> CreateGate(CreateGateCommand command)
        {
            var result = await _gateService.CreateGate(command);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut()]
        public async Task<IActionResult> UpdateGate( GateUpdateCommand command)
        {
            var result = await _gateService.UpdateGate( command);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpDelete("{gateId}")]
        public async Task<IActionResult> DeleteGate(int gateId)
        {
            var result = await _gateService.DeleteGate(gateId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("TestTime")]
        public ActionResult TestTime()
        {
            return Ok(DateTime.Now);
        }
    }
}
