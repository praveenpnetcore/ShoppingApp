using ShoppingApp.Models;
using ShoppingApp.ViewModels;

namespace ShoppingApp.Services
{
    public interface ICartService
    {
        Task<IEnumerable<CartItem>> GetCartItemsAsync(string sessionId);
        Task<CartSummaryViewModel> GetCartSummaryAsync(string sessionId);
        Task AddToCartAsync(string sessionId, int productId, int quantity = 1);
        Task UpdateCartItemAsync(int cartItemId, int quantity);
        Task RemoveFromCartAsync(int cartItemId);
        Task ClearCartAsync(string sessionId);
        Task<int> GetCartItemCountAsync(string sessionId);
    }
}
