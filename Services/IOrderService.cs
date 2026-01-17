using ShoppingApp.Models;
using ShoppingApp.ViewModels;

namespace ShoppingApp.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(CheckoutViewModel checkout, string sessionId);
        Task<Order?> GetOrderByIdAsync(int id);
        Task<Order?> GetOrderByNumberAsync(string orderNumber);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetUserOrdersAsync(string userId);
        Task UpdateOrderStatusAsync(int orderId, OrderStatus status);
    }
}
