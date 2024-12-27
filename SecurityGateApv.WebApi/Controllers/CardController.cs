using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using SecurityGateApv.Application.DTOs.Req;
using SecurityGateApv.Application.DTOs.Req.CreateReq;
using SecurityGateApv.Application.Services;
using SecurityGateApv.Application.Services.Interface;

namespace SecurityGateApv.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        //[HttpPost("decode")]
        //public async Task<IActionResult> DecodeQRCode( IFormFile image)
        //{
        //    // Kiểm tra xem file có tồn tại không
        //    if (image == null || image.Length == 0)
        //    {
        //        return BadRequest("Vui lòng chọn một file ảnh.");
        //    }

        //    try
        //    {
        //        var result = _qrCodeService.DecodeQRCodeFromImage(image);
        //        return Ok(new { Text = result });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Lỗi: {ex.Message}");
        //    }
        //}
        [HttpGet()]
        public async Task<ActionResult> GetAllQrCardPaging(int pageNumber,  int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var result = await _cardService.GetAllByPaging(pageNumber, pageSize);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("{cardVerification}")]
        public async Task<ActionResult> GetQrCardByCardVerification(string cardVerification)
        {
            if (cardVerification == null)
            {
                return BadRequest("CardVerification can not null");
            }

            var result = await _cardService.GetQrCardByCardVerification(cardVerification);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("CredentialCard/{credentialCard}")]
        public async Task<ActionResult> GetQrCardByCredentialCard(string credentialCard)
        {
            if (credentialCard == null)
            {
                return BadRequest("CredentialCard can not null");
            }

            var result = await _cardService.GetQrCardByCardCredentialCard(credentialCard);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost("ShoeDetect")]
        public async Task<IActionResult> ShoeDetect(DetectImageCommand request)
        {
            // Kiểm tra xem file có tồn tại không
            if (request.Image == null || request.Image.Length == 0)
            {
                return BadRequest("Vui lòng chọn một file ảnh.");
            }

            try
            {
                var result = await _cardService.DetectShoe(request.Image);
                if (result.IsFailure)
                {
                    return BadRequest(result.Error);
                }
                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }

        [HttpPost("GenerateCard")]
        public async Task<IActionResult> GenerateCard(CreateCardCommand command)
        {
            var result = await _cardService.GenerateCard(command);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateCard( CreateCardCommand command)
        {
            var result = await _cardService.CreateCard(command);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost("UpdateCardLost")]
        public async Task<ActionResult> UpdateCardStatusLost(string credentialCard, string newQrCardVerifed)
        {
            var result = await _cardService.UpdateCardStatusLost(credentialCard, newQrCardVerifed);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        //[HttpPost("LostCard/{visitDetailId}")]
        //public async Task<ActionResult> UpdateCardStatusLost(int visitDetailId)
        //{
        //    var result = await _cardService.UpdateCardStatusLost(visitDetailId);

        //    if (result.IsFailure)
        //    {
        //        return BadRequest(result.Error);
        //    }
        //    return Ok(result.Value);
        //}
    }
}
