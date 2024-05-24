using EbisconDemo.Api.Extensions;
using EbisconDemo.Api.Models;
using EbisconDemo.Data.Models;
using EbisconDemo.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace EbisconDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> OrderProductAsync([FromBody] OrderApiModel model)
        {
            var creationModel = new CreateOrderDto
            {
                ProductId = model.ProductId!.Value,
                Count = model.Count!.Value,
                UserId = User.GetCurrentUserId()
            };

            var order = await _orderService.OrderProductAsync(creationModel);

            return Ok(order);
        }

        [HttpPut]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> SetOrderStatusAsync(int orderId, string status)
        {
            if (orderId <= 0 || string.IsNullOrWhiteSpace(status))
            {
                return BadRequest();
            }

            await _orderService.SetStatusAsync(orderId, status);

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> GetOrderAsnc(int id)
        {
            if (id <= 0)
            {
                return NotFound("Id must be greater than 0.");
            }

            var order = await _orderService.GetOrderAsync(id);

            return Ok(order);
        }
    }
}
