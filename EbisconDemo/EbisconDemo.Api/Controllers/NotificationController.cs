using EbisconDemo.Api.Extensions;
using EbisconDemo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbisconDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetNotificationsAsync() 
        {
            // Also for notifications SignalR can be used in case the project is not just an API.

            var userId = User.GetCurrentUserId();
            var notifications = await _notificationService.GetNotificationsAsync(userId);

            if(notifications == null)
            {
                return NoContent();
            }

            return Ok(notifications);
        }

        [HttpPut("SetRead")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> SetReadAsync(int notificationId)
        {
            if (notificationId <= 0) 
            {
                return BadRequest("Invalid notification ID.");
            }

            await _notificationService.SetReadAsync(notificationId);

            return Ok();
        }
    }
}
