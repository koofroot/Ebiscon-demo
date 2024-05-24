using EbisconDemo.Data.Models;
using EbisconDemo.Services.Models;

namespace EbisconDemo.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();

        Task<OrderDto> OrderProductAsync(CreateOrderDto model);

        Task<OrderDto> GetOrderAsync(int id);
        Task SetStatusAsync(int orderId, string status);
    }
}
