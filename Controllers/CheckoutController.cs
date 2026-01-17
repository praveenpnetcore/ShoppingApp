using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services;
using ShoppingApp.ViewModels;

namespace ShoppingApp.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CheckoutController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        private string GetSessionId()
        {
            return HttpContext.Session.GetString("CartSessionId") ?? string.Empty;
        }

        public async Task<IActionResult> Index()
        {
            var sessionId = GetSessionId();
            if (string.IsNullOrEmpty(sessionId))
            {
                return RedirectToAction("Index", "Cart");
            }

            var cartSummary = await _cartService.GetCartSummaryAsync(sessionId);
            if (!cartSummary.CartItems.Any())
            {
                TempData["Error"] = "Your cart is empty. Please add items before checkout.";
                return RedirectToAction("Index", "Cart");
            }

            var viewModel = new CheckoutViewModel
            {
                CartSummary = cartSummary
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(CheckoutViewModel model)
        {
            var sessionId = GetSessionId();
            if (string.IsNullOrEmpty(sessionId))
            {
                return RedirectToAction("Index", "Cart");
            }

            var cartSummary = await _cartService.GetCartSummaryAsync(sessionId);
            model.CartSummary = cartSummary;

            if (!cartSummary.CartItems.Any())
            {
                TempData["Error"] = "Your cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            try
            {
                var order = await _orderService.CreateOrderAsync(model, sessionId);
                TempData["Success"] = "Order placed successfully!";
                return RedirectToAction("Confirmation", new { orderNumber = order.OrderNumber });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing your order. Please try again.");
                return View("Index", model);
            }
        }

        public async Task<IActionResult> Confirmation(string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
            {
                return RedirectToAction("Index", "Home");
            }

            var order = await _orderService.GetOrderByNumberAsync(orderNumber);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
