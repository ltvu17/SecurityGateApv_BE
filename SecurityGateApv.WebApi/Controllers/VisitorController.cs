using Microsoft.AspNetCore.Mvc;
using SecurityGateApv.Application.DTOs.Req.CreateReq;
using SecurityGateApv.Application.DTOs.Req.UpdateReq;
using SecurityGateApv.Application.Services;
using SecurityGateApv.Application.Services.Interface;

namespace SecurityGateApv.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : Controller
    {
        private readonly IVisitorService _visitorService;

        //private readonly IVisitorService _visitorService
        public VisitorController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllVisitor(int pageNumber, int pageSize)
        {
            if (pageNumber == -1 || pageSize == -1)
            {
                var resultAll = await _visitorService.GetAllByPaging(1, int.MaxValue);
                return Ok(resultAll.Value);
            }
            var result = await _visitorService.GetAllByPaging(pageNumber, pageSize);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("{visitorId}")]
        public async Task<IActionResult> GetVisitorById(int visitorId)
        {
            var result = await _visitorService.GetById(visitorId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("CredentialCard/{creadentialCard}")]
        public async Task<IActionResult> GetVisitorByCreadentialCard(string creadentialCard)
        {
            var result = await _visitorService.GetByCredentialCard(creadentialCard);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateVisitor([FromForm] CreateVisitorCommand command)
        {
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new Error("CreateVisitor", "Invalid Token"));
            }
            var result = await _visitorService.CreateVisitor(command, token);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("{visitorId}")]
        public async Task<IActionResult> UpdateVisitor(int visitorId, [FromForm] UpdateVisitorCommand command)
        {
            var result = await _visitorService.UpdateVisitor(visitorId, command);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpDelete("{visitorId}")]
        public async Task<IActionResult> DeleteVisitor(int visitorId)
        {
            var result = await _visitorService.DeleteVisitor(visitorId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
