using Microsoft.AspNetCore.Mvc;
using SecurityGateApv.Application.Services.Interface;

namespace SecurityGateApv.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly INotificationsService _notificationsService;

        public NotificationController(INotificationsService notificationsService)
        {
            _notificationsService = notificationsService;
        }
        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetNotificationByUserId(int userId)
        {
            var result = await _notificationsService.GetNotificationOfUser(userId, int.MaxValue, 1);
            if (result.IsFailure)
            {
                return NotFound();
            }
            return Ok(result.Value);
        }
        [HttpGet("NotificationUser/IsRead/{notificationUserId}")]
        public async Task<IActionResult> MarkNotificationRead(int notificationUserId)
        {
            var result = await _notificationsService.MarkNotificationRead(notificationUserId);
            if (result.IsFailure)
            {
                return NotFound();
            }
            return Ok(result.Value);
        }
    }
}
