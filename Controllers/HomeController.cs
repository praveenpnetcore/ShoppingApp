using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services;
using ShoppingApp.ViewModels;

namespace ShoppingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IDiscountService _discountService;

        public HomeController(
            IProductService productService,
            ICategoryService categoryService,
            IDiscountService discountService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _discountService = discountService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetActiveProductsAsync();
            var categories = await _categoryService.GetActiveCategoriesAsync();
            var discountSetting = await _discountService.GetActiveDiscountSettingAsync();

            var viewModel = new HomeViewModel
            {
                FeaturedProducts = products.Take(8),
                Categories = categories,
                DiscountPercentage = discountSetting?.DiscountPercentage ?? 10,
                MinimumAmountForDiscount = discountSetting?.MinimumAmountThreshold ?? 5000
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
