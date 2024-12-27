using Microsoft.AspNetCore.Mvc;
using SecurityGateApv.Application.Services.Interface;

namespace SecurityGateApv.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockController : Controller
    {
        private readonly IScriptService _scriptService;

        public MockController(IScriptService scriptService)
        {
            _scriptService = scriptService;
        }
        [HttpGet("Coflow1_1_CreateVisit")]
        public async Task<IActionResult> Coflow1_1_CreateVisit(int requestOfStaff, int requestOfSecurity)
        {
            var result = await _scriptService.Coflow1_1_CreateVisit(requestOfStaff, requestOfSecurity);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("Coflow1_1_MockVisitor")]
        public async Task<IActionResult> Coflow1_1_MockVisitor(int numberOfVisitor)
        {
            var result = await _scriptService.Coflow1_1_MockVisitor(numberOfVisitor);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("Coflow1_2_RejectVisitOfSecurity")]
        public async Task<IActionResult> Coflow1_2_RejectVisitOfSecurityAndCancelVisit()
        {
            var result1 = await _scriptService.Coflow1_3_CancelVisit(1);
            if (result1.IsFailure)
            {
                return BadRequest(result1.Error);
            }
            var result2 = await _scriptService.Coflow1_2_RejectVisitOfSecurity(1);
            if (result2.IsFailure)
            {
                return BadRequest(result2.Error);
            }
            return Ok(result2.Value);
        }
        [HttpGet("Coflow2_1_CreateSchedule")]
        public async Task<IActionResult> Coflow2_1_CreateSchedule(int numberRequestOfSchedule)
        {
            var result = await _scriptService.Coflow2_1_CreateSchedule(numberRequestOfSchedule);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            var result2 = await _scriptService.Coflow2_2_AssignScheduleForStaff(numberRequestOfSchedule);
            if (result2.IsFailure)
            {
                return BadRequest(result.Error);
            }
            var result3 = await _scriptService.Coflow2_3_CreateScheduleAssign(numberRequestOfSchedule);
            if (result3.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        //[HttpGet("Coflow2_2_AssignScheduleForStaff")]
        //public async Task<IActionResult> Coflow2_2_AssignScheduleForStaff(int numberRequestOfSchedule)
        //{
        //    var result = await _scriptService.Coflow2_2_AssignScheduleForStaff(numberRequestOfSchedule);
        //    if (result.IsFailure)
        //    {
        //        return BadRequest(result.Error);
        //    }
        //    return Ok(result.Value);
        //}
        //[HttpGet("Coflow2_3_CreateScheduleAssign")]
        //public async Task<IActionResult> Coflow2_3_CreateScheduleAssign(int numberRequestOfSchedule)
        //{
        //    var result = await _scriptService.Coflow2_2_AssignScheduleForStaff(numberRequestOfSchedule);
        //    if (result.IsFailure)
        //    {
        //        return BadRequest(result.Error);
        //    }
        //    return Ok(result.Value);
        //}        
        [HttpGet("Coflow2_4_AcceptAndRejectTask")]
        public async Task<IActionResult> Coflow2_4_AcceptAndRejectTask(int numberOfReject, int numberOfAccept)
        {
            var result = await _scriptService.Coflow2_4_AcceptAndRejectTask(numberOfReject, numberOfAccept);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }        
        [HttpGet("Coflow3_1_Check_in")]
        public async Task<IActionResult> Coflow3_1_Check_in(int numberOfCheckIn)
        {
            var result = await _scriptService.Coflow3_1_Check_in(numberOfCheckIn);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }      
        [HttpGet("Coflow4_GetListCheckIn")]
        public async Task<IActionResult> Coflow4_GetListCheckIn()
        {
            var result = await _scriptService.Coflow4_GetListCheckIn();
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("Coflow4_1_Check_out")]
        public async Task<IActionResult> Coflow4_1_Check_out(int checkOutNumber, int checkOutLostNumber)
        {
            var result = await _scriptService.Coflow4_1_Check_out(checkOutNumber);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }        
        //[HttpGet("Coflow4_2_Check_out_lost_card")]
        //public async Task<IActionResult> Coflow4_2_Check_out_lost_card(int checkInId)
        //{
        //    var result = await _scriptService.Coflow4_2_Check_out_lost_card(checkInId);
        //    if (result.IsFailure)
        //    {
        //        return BadRequest(result.Error);
        //    }
        //    return Ok(result.Value);
        //}
    }
}
