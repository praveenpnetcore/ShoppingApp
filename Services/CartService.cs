using Microsoft.EntityFrameworkCore;
using ShoppingApp.Models;
using ShoppingApp.Repositories;
using ShoppingApp.ViewModels;

namespace ShoppingApp.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository<CartItem> _cartRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IDiscountService _discountService;

        public CartService(
            IRepository<CartItem> cartRepository,
            IRepository<Product> productRepository,
            IDiscountService discountService)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _discountService = discountService;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync(string sessionId)
        {
            return await _cartRepository.Query()
                .Include(c => c.Product)
                    .ThenInclude(p => p!.Category)
                .Where(c => c.SessionId == sessionId)
                .ToListAsync();
        }

        public async Task<CartSummaryViewModel> GetCartSummaryAsync(string sessionId)
        {
            var cartItems = await GetCartItemsAsync(sessionId);
            var discountSetting = await _discountService.GetActiveDiscountSettingAsync();

            var cartItemViewModels = new List<CartItemViewModel>();
            decimal subTotal = 0;
            decimal totalDiscount = 0;

            foreach (var item in cartItems)
            {
                if (item.Product == null) continue;

                var itemTotal = item.Product.Price * item.Quantity;
                var itemDiscount = 0m;

                // Apply discount if product price is above threshold
                if (discountSetting != null && item.Product.Price >= discountSetting.MinimumAmountThreshold)
                {
                    itemDiscount = (item.Product.Price * discountSetting.DiscountPercentage / 100) * item.Quantity;
                }

                subTotal += itemTotal;
                totalDiscount += itemDiscount;

                cartItemViewModels.Add(new CartItemViewModel
                {
                    CartItemId = item.Id,
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    ProductImage = item.Product.ImageUrl,
                    UnitPrice = item.Product.Price,
                    Quantity = item.Quantity,
                    TotalPrice = itemTotal,
                    DiscountAmount = itemDiscount,
                    FinalPrice = itemTotal - itemDiscount,
                    IsDiscountApplicable = itemDiscount > 0,
                    DiscountPercentage = discountSetting?.DiscountPercentage ?? 0
                });
            }

            return new CartSummaryViewModel
            {
                CartItems = cartItemViewModels,
                SubTotal = subTotal,
                TotalDiscount = totalDiscount,
                GrandTotal = subTotal - totalDiscount,
                DiscountPercentage = discountSetting?.DiscountPercentage ?? 0,
                MinimumAmountForDiscount = discountSetting?.MinimumAmountThreshold ?? 5000,
                TotalItems = cartItemViewModels.Sum(x => x.Quantity)
            };
        }

        public async Task AddToCartAsync(string sessionId, int productId, int quantity = 1)
        {
            var existingItem = await _cartRepository.GetFirstOrDefaultAsync(
                c => c.SessionId == sessionId && c.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                await _cartRepository.UpdateAsync(existingItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    SessionId = sessionId,
                    ProductId = productId,
                    Quantity = quantity,
                    AddedDate = DateTime.Now
                };
                await _cartRepository.AddAsync(cartItem);
            }
        }

        public async Task UpdateCartItemAsync(int cartItemId, int quantity)
        {
            var cartItem = await _cartRepository.GetByIdAsync(cartItemId);
            if (cartItem != null)
            {
                if (quantity <= 0)
                {
                    await _cartRepository.DeleteAsync(cartItem);
                }
                else
                {
                    cartItem.Quantity = quantity;
                    await _cartRepository.UpdateAsync(cartItem);
                }
            }
        }

        public async Task RemoveFromCartAsync(int cartItemId)
        {
            var cartItem = await _cartRepository.GetByIdAsync(cartItemId);
            if (cartItem != null)
            {
                await _cartRepository.DeleteAsync(cartItem);
            }
        }

        public async Task ClearCartAsync(string sessionId)
        {
            var cartItems = await _cartRepository.GetAllAsync(c => c.SessionId == sessionId);
            foreach (var item in cartItems)
            {
                await _cartRepository.DeleteAsync(item);
            }
        }

        public async Task<int> GetCartItemCountAsync(string sessionId)
        {
            var items = await _cartRepository.GetAllAsync(c => c.SessionId == sessionId);
            return items.Sum(c => c.Quantity);
        }
    }
}
