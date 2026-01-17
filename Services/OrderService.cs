using Microsoft.EntityFrameworkCore;
using ShoppingApp.Models;
using ShoppingApp.Repositories;
using ShoppingApp.ViewModels;

namespace ShoppingApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly ICartService _cartService;

        public OrderService(IRepository<Order> orderRepository, ICartService cartService)
        {
            _orderRepository = orderRepository;
            _cartService = cartService;
        }

        public async Task<Order> CreateOrderAsync(CheckoutViewModel checkout, string sessionId)
        {
            var cartSummary = await _cartService.GetCartSummaryAsync(sessionId);

            var order = new Order
            {
                OrderNumber = GenerateOrderNumber(),
                CustomerName = checkout.CustomerName,
                Email = checkout.Email,
                Phone = checkout.Phone,
                ShippingAddress = checkout.ShippingAddress,
                City = checkout.City,
                State = checkout.State,
                PinCode = checkout.PinCode,
                SubTotal = cartSummary.SubTotal,
                DiscountAmount = cartSummary.TotalDiscount,
                DiscountPercentage = cartSummary.DiscountPercentage,
                TotalAmount = cartSummary.GrandTotal,
                PaymentMethod = checkout.PaymentMethod,
                Status = OrderStatus.Pending,
                OrderDate = DateTime.Now,
                OrderItems = cartSummary.CartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    TotalPrice = item.FinalPrice
                }).ToList()
            };

            await _orderRepository.AddAsync(order);
            await _cartService.ClearCartAsync(sessionId);

            return order;
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.Query()
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order?> GetOrderByNumberAsync(string orderNumber)
        {
            return await _orderRepository.Query()
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.Query()
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(string userId)
        {
            return await _orderRepository.Query()
                .Include(o => o.OrderItems)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                order.UpdatedDate = DateTime.Now;
                await _orderRepository.UpdateAsync(order);
            }
        }

        private string GenerateOrderNumber()
        {
            return $"ORD{DateTime.Now:yyyyMMdd}{DateTime.Now.Ticks.ToString().Substring(10, 6)}";
        }
    }
}
