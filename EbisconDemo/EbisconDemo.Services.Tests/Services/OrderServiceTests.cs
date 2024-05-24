using AutoMapper;
using EbisconDemo.Data.Interfaces;
using EbisconDemo.Data.Models;
using EbisconDemo.Services.Interfaces;
using EbisconDemo.Services.Models;
using EbisconDemo.Services.Services;
using Moq;

namespace EbisconDemo.Services.Tests.Services
{
    [TestClass]
    public class OrderServiceTests
    {
        private readonly OrderService _orderService;

        private readonly Mock<INotificationService> _notificationService;
        private readonly Mock<IRepository<Product>> _productRepository;
        private readonly Mock<IRepository<Order>> _orderRepository;
        private readonly Mock<IMapper> _mapper;
        public OrderServiceTests()
        {
            _notificationService = new Mock<INotificationService>();
            _productRepository = new Mock<IRepository<Product>>();
            _orderRepository = new Mock<IRepository<Order>>();
            _mapper = new Mock<IMapper>();

            _orderService = new OrderService(
                _notificationService.Object,
                _productRepository.Object,
                _orderRepository.Object,
                _mapper.Object);
        }

        [TestMethod]
        public async Task GetAllOrders_WhenNoOrders_ShouldReturnNull()
        {
            _orderRepository.Setup(x => x.GetAllAsync())
                .Returns(Task.FromResult(new List<Order>().AsEnumerable()));

            var expected = await _orderService.GetAllOrdersAsync();

            Assert.IsNull(expected);
        }

        [TestMethod]
        public async Task GetAllOrders_WhenHasOrders_ShouldMapper()
        {
            var orders = new List<Order>() { 
                new Order() { Id = 1 },
                new Order() { Id = 2 },
                new Order() { Id = 2 }
            };
            _orderRepository.Setup(x => x.GetAllAsync())
               .Returns(Task.FromResult(orders.AsEnumerable()));
            _mapper.Setup(x => x.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders));

            var expected = await _orderService.GetAllOrdersAsync();

            _mapper.Verify(x => x.Map<IEnumerable<OrderDto>>(orders));
        }
    }
}
