using Microsoft.AspNetCore.Mvc;
using SecurityGateApv.Application.DTOs.Req;
using SecurityGateApv.Application.Services;
using SecurityGateApv.Application.Services.Interface;

namespace SecurityGateApv.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitCardController : Controller
    {
        private readonly IVisitCardService _visitCardService;
        public VisitCardController(IVisitCardService visitCardService)
        {
            _visitCardService = visitCardService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateVisitCard(int  visitDetailId,  string cardVerification)
        {
            var result = await _visitCardService.CreateVisitCard(visitDetailId, cardVerification);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        //[HttpPost("LostCard")]
        //public async Task<ActionResult> CreateVisitCard(int visitDetailId, string cardVerification)
        //{
        //    var result = await _visitCardService.CreateVisitCard(visitDetailId, cardVerification);

        //    if (result.IsFailure)
        //    {
        //        return BadRequest(result.Error);
        //    }
        //    return Ok(result.Value);
        //}
    }
}
