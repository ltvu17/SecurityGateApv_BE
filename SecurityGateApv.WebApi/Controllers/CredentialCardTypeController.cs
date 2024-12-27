using Microsoft.AspNetCore.Mvc;
using SecurityGateApv.Application.Services.Interface;

namespace SecurityGateApv.WebApi.Controllers
{
    public class CredentialCardTypeController : Controller
    {
        private readonly ICredentialCardTypeService _credentialCardTypeService;

        public CredentialCardTypeController(ICredentialCardTypeService CredentialCardTypeService)
        {
            _credentialCardTypeService = CredentialCardTypeService;
        }
        public async Task<IActionResult> GetAll()
        {
            var result = await _credentialCardTypeService.GetAll();
            if(result == null)
            {
                return BadRequest(result.Error);
            }
            return Ok(result);
        }
    }
}
