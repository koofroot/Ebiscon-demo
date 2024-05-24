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
        private readonly IValidator<OrderApiModel> _orderValidator;
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public IActionResult OrderProduct([FromBody] OrderApiModel model)
        {
            var creationModel = new CreateOrderDto
            {
                ProductId = model.ProductId!.Value,
                Count = model.Count!.Value,
                UserId = User.GetCurrentUserId()
            };

            var order = _orderService.OrderProduct(creationModel);

            return Ok(order);
        }

        [HttpPut]
        [Authorize(Roles = "Manager")]
        public IActionResult SetOrderStatus(int orderId, string status)
        {
            if (orderId <= 0 || string.IsNullOrWhiteSpace(status))
            {
                return BadRequest();
            }

            _orderService.SetStatus(orderId, status);

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Manager,Admin")]
        public IActionResult Orders()
        {
            var orders = _orderService.GetAllOrders();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Manager,Admin")]
        public IActionResult GetOrder(int id)
        {
            if (id <= 0)
            {
                return NotFound("Id must be greater than 0.");
            }

            var order = _orderService.GetOrder(id);

            return Ok(order);
        }
    }
}
