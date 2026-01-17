using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services;

namespace ShoppingApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        private string GetSessionId()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CartSessionId")))
            {
                HttpContext.Session.SetString("CartSessionId", Guid.NewGuid().ToString());
            }
            return HttpContext.Session.GetString("CartSessionId")!;
        }

        public async Task<IActionResult> Index()
        {
            var sessionId = GetSessionId();
            var cartSummary = await _cartService.GetCartSummaryAsync(sessionId);
            return View(cartSummary);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return Json(new { success = false, message = "Product not found" });
            }

            if (product.StockQuantity < quantity)
            {
                return Json(new { success = false, message = "Insufficient stock" });
            }

            var sessionId = GetSessionId();
            await _cartService.AddToCartAsync(sessionId, productId, quantity);
            var cartCount = await _cartService.GetCartItemCountAsync(sessionId);

            return Json(new { 
                success = true, 
                message = $"{product.Name} added to cart!",
                cartCount = cartCount
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            await _cartService.UpdateCartItemAsync(cartItemId, quantity);
            var sessionId = GetSessionId();
            var cartSummary = await _cartService.GetCartSummaryAsync(sessionId);

            return Json(new { 
                success = true, 
                cartSummary = cartSummary 
            });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            await _cartService.RemoveFromCartAsync(cartItemId);
            var sessionId = GetSessionId();
            var cartSummary = await _cartService.GetCartSummaryAsync(sessionId);

            return Json(new { 
                success = true, 
                message = "Item removed from cart",
                cartSummary = cartSummary 
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetCartCount()
        {
            var sessionId = GetSessionId();
            var count = await _cartService.GetCartItemCountAsync(sessionId);
            return Json(new { count });
        }

        [HttpGet]
        public async Task<IActionResult> GetCartSummary()
        {
            var sessionId = GetSessionId();
            var cartSummary = await _cartService.GetCartSummaryAsync(sessionId);
            return Json(cartSummary);
        }
    }
}
