using EbisconDemo.Api.Models;
using EbisconDemo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EbisconDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ISynchronizationService _synchronizationService;
        private readonly IUserService _userService;

        public DashboardController(
            ISynchronizationService synchronizationService, 
            IUserService userService)
        {
            _synchronizationService = synchronizationService;
            _userService = userService;
        }

        [HttpGet("Sync")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> SyncAsync()
        {
            await _synchronizationService.SyncProductsAsync();

            return Ok();
        }

        [HttpPut("SetRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetRoleAsync([FromBody] SetRoleApiModel model)
        {
            await _userService.SetUserRoleAsync(model.UserEmail, model.Role);

            return Ok();
        }
    }
}
