using AutoMapper;
using EbisconDemo.Data.Interfaces;
using EbisconDemo.Data.Models;
using EbisconDemo.Data.Models.Enums;
using EbisconDemo.Services.Exceptions;
using EbisconDemo.Services.Interfaces;
using EbisconDemo.Services.Models;

namespace EbisconDemo.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly INotificationService _notificationService;
        private readonly IRepository<Product> _productRepository; 
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(
            INotificationService notificationService,
            IRepository<Product> productRepository,
            IRepository<Order> orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            if(orders == null || !orders.Any())
            {
                return null!;
            }

            var mapped = _mapper.Map<IEnumerable<OrderDto>>(orders);

            return mapped;
        }

        public async Task<OrderDto> GetOrderAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("id must be greater than 0.", nameof(id));
            }

            var order = await _orderRepository.GetAsync(id);

            var mapped = _mapper.Map<OrderDto>(order);

            return mapped;
        }

        public async Task<OrderDto> OrderProductAsync(CreateOrderDto model)
        {
            if(model.ProductId <= 0 || model.Count <= 0 || model.UserId <= 0)
            {
                throw new ArgumentException("Product ID, User ID or Count is incorrect.");
            }

            var product = await _productRepository.GetAsync(model.ProductId);

            if(product == null)
            {
                throw new ProductNotFoundException("Product with that ID is not found.");
            }

            var order = _mapper.Map<Order>(model);
            
            await _orderRepository.CreateAsync(order);

            await _orderRepository.SaveAsync();

            await _notificationService.NotifyOrderCreatedAsync(order.Id, "New order has been created.");

            var createdOrder = await _orderRepository.GetAsync(order.Id);

            var mapped = _mapper.Map<Order, OrderDto>(createdOrder!);

            return mapped;
        }

        public async Task SetStatusAsync(int orderId, string status)
        {
            var isStatusValid = Enum.TryParse<OrderStatus>(status, true, out var orderStatus);
            var order = await _orderRepository.GetAsync(orderId);

            if(!isStatusValid || order == null)
            {                
                throw new SetStatusException();
            }

            order.Status = orderStatus;

            await _orderRepository.GetAllAsync();
        }
    }
}
