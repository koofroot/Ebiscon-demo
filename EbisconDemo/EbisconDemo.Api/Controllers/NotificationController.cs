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
        public IActionResult GetNotifications() 
        {
            // Also for notifications SignalR can be used in case the project is not just an API.

            var userId = User.GetCurrentUserId();
            var notifications = _notificationService.GetNotifications(userId);

            if(notifications == null)
            {
                return NoContent();
            }

            return Ok(notifications);
        }

        [HttpPut("SetRead")]
        [Authorize(Roles = "Manager")]
        public IActionResult SetRead(int notificationId)
        {
            if (notificationId <= 0) 
            {
                return BadRequest("Invalid notification ID.");
            }

            _notificationService.SetRead(notificationId);

            return Ok();
        }
    }
}
