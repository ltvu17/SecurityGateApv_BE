using Microsoft.AspNetCore.Mvc;
using SecurityGateApv.Application.DTOs.Req;
using SecurityGateApv.Application.Services;
using SecurityGateApv.Application.Services.Interface;
using SecurityGateApv.Domain.Models;

namespace SecurityGateApv.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorSessionController : Controller
    {
        private readonly IVisitorSessionService _visitorSessionService;
        public VisitorSessionController(IVisitorSessionService visitorSessionService)
        {
            _visitorSessionService = visitorSessionService;
        }
        [HttpPut("CheckOutWithCard")]
        public async Task<ActionResult> CheckOutWithCard(VisitorSessionCheckOutCommand command, string qrCardVerifi)
        {
            var result = await _visitorSessionService.CheckOutWithCard(command, qrCardVerifi);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("CheckOutWithCredentialCard")]
        public async Task<ActionResult> CheckOutWithCredentialCard(VisitorSessionCheckOutCommand command, string credentialCard)
        {
            var result = await _visitorSessionService.CheckOutWithCredentialCard(command, credentialCard);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }


        [HttpPost("CheckIn")]
        public async Task<ActionResult> CheckIn([FromForm] VisitSessionCheckInCommand command)
        {
            var result = await _visitorSessionService.CheckInWithCredentialCard(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
            //if (!string.IsNullOrEmpty(command.CredentialCard))
            //{
            //}
            //else
            //{
            //    var result = await _visitorSessionService.CheckInWithoutCredentialCard(command);
            //    if (result.IsSuccess)
            //    {
            //        return Ok(result.Value);
            //    }
            //    return BadRequest(result.Error);
            //}
        }
        [HttpPost("ValidCheckIn")]
        public async Task<ActionResult> ValidCheckIn([FromForm] ValidCheckInCommand command)
        {
            if (!string.IsNullOrEmpty(command.QRCardVerification))
            {
                var result = await _visitorSessionService.ValidCheckWithQRCardVerification(command);

                if (result.IsFailure)
                {
                    return BadRequest(result.Error);
                }
                return Ok(result.Value);
            }
            else
            {
                var result = await _visitorSessionService.ValidCheckWithoutQRCardVerification(command);
                if (result.IsSuccess)
                {
                    return Ok(result.Value);
                }
                return BadRequest(result.Error);
            }
        }
        [HttpGet()]
        public async Task<IActionResult> GetAllVisitorSession(int pageNumber, int pageSize)
        {
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new Error("CreateUser", "Invalid Token"));
            }
            if (pageNumber == -1 || pageSize == -1)
            {
                var resultAll = await _visitorSessionService.GetAllVisitorSession(1, int.MaxValue, token);
                return Ok(resultAll.Value);
            }
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var result = await _visitorSessionService.GetAllVisitorSession(pageNumber, pageSize, token);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("Date")]
        public async Task<IActionResult> GetVisitorSessionByDate(int pageNumber, int pageSize, DateTime date)
        {
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new Error("CreateUser", "Invalid Token"));
            }
            if (pageNumber == -1 || pageSize == -1)
            {
                var resultAll = await _visitorSessionService.GetVisitorSessionByDate(1, int.MaxValue, date, token);
                return Ok(resultAll.Value);
            }
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var result = await _visitorSessionService.GetVisitorSessionByDate(pageNumber, pageSize, date, token);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("Visitor/{visitorId}")]
        public async Task<IActionResult> GetAllVisitorSessionByVisitorId(int pageNumber, int pageSize, int visitorId)
        {
            if (pageNumber == -1 || pageSize == -1)
            {
                var resultAll = await _visitorSessionService.GetAllVisitorSessionByVisitorId(1, int.MaxValue, visitorId);
                return Ok(resultAll.Value);
            }
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var result = await _visitorSessionService.GetAllVisitorSessionByVisitorId(pageNumber, pageSize, visitorId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
            return Ok(result.Value);
        } 
        [HttpGet("Images/{visitorSessionId}")]
        public async Task<IActionResult> GetAllImagesByVisitorSessionId( int visitorSessionId)
        {
            
            var result = await _visitorSessionService.GetAllImagesByVisitorSessionId( visitorSessionId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("VehicleSession/{visitorSessionId}")]
        public async Task<IActionResult> GetVehicleSessionByvisitorId(int visitorSessionId)
        {

            var result = await _visitorSessionService.GetVehicleSessionByvisitorId(visitorSessionId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("VehicleSession/Visit/{visitId}")]
        public async Task<IActionResult> GetVehicleSessionByvisitId(int visitId)
        {

            var result = await _visitorSessionService.GetVehicleSessionByVisitId(visitId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }[HttpGet("VehicleSession/Visit/{visitId}/Visitor/{visitorId}")]
        public async Task<IActionResult> GetVehicleSessionByvisitorId(int visitId, int visitorId)
        {

            var result = await _visitorSessionService.GetVehicleSessionByVisitorId(visitId, visitorId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("Visit/{visitId}")]
        public async Task<IActionResult> GetAllVisitorSessionByVisitId(int pageNumber, int pageSize, int visitId)
        {
            if (pageNumber == -1 || pageSize == -1)
            {
                var resultAll = await _visitorSessionService.GetAllVisitorSessionByVisitId(1, int.MaxValue, visitId);
                return Ok(resultAll.Value);
            }
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var result = await _visitorSessionService.GetAllVisitorSessionByVisitId(pageNumber, pageSize, visitId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("StatusCheckIn/Card/{cardVerified}")]
        public async Task<IActionResult> GetVisitSessionStatusCheckInByCardVerification(string cardVerified)
        {
            var result = await _visitorSessionService.GetVisitSessionStatusCheckInByCardVerification(cardVerified);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("StatusCheckIn/CredentialId/{credentialId}")]
        public async Task<IActionResult> GetVisitorSessionStatusCheckInByCredentialIdId(string credentialId)
        {
            var result = await _visitorSessionService.GetVisitorSessionStatusCheckInByCredentialIdId(credentialId);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
