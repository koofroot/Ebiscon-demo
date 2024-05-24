using EbisconDemo.Data.Models;
using EbisconDemo.Services.Models;

namespace EbisconDemo.Services.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> GetAllOrders();

        OrderDto OrderProduct(CreateOrderDto model);

        OrderDto GetOrder(int id);
        void SetStatus(int orderId, string status);
    }
}
