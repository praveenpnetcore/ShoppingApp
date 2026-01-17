using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services;
using ShoppingApp.ViewModels;

namespace ShoppingApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IDiscountService _discountService;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            IDiscountService discountService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _discountService = discountService;
        }

        public async Task<IActionResult> Index(int? categoryId, string? search)
        {
            var categories = await _categoryService.GetActiveCategoriesAsync();
            var discountSetting = await _discountService.GetActiveDiscountSettingAsync();

            IEnumerable<Models.Product> products;

            if (!string.IsNullOrEmpty(search))
            {
                products = await _productService.SearchProductsAsync(search);
            }
            else if (categoryId.HasValue)
            {
                products = await _productService.GetProductsByCategoryAsync(categoryId.Value);
            }
            else
            {
                products = await _productService.GetActiveProductsAsync();
            }

            var viewModel = new ProductListViewModel
            {
                Products = products,
                Categories = categories,
                SelectedCategoryId = categoryId,
                SearchTerm = search,
                DiscountPercentage = discountSetting?.DiscountPercentage ?? 10,
                MinimumAmountForDiscount = discountSetting?.MinimumAmountThreshold ?? 5000
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var discountSetting = await _discountService.GetActiveDiscountSettingAsync();
            var relatedProducts = await _productService.GetProductsByCategoryAsync(product.CategoryId);

            var isDiscountApplicable = discountSetting != null && 
                product.Price >= discountSetting.MinimumAmountThreshold;
            
            var discountedPrice = isDiscountApplicable 
                ? product.Price - (product.Price * discountSetting!.DiscountPercentage / 100)
                : product.Price;

            var viewModel = new ProductDetailViewModel
            {
                Product = product,
                RelatedProducts = relatedProducts.Where(p => p.Id != id).Take(4),
                DiscountPercentage = discountSetting?.DiscountPercentage ?? 10,
                MinimumAmountForDiscount = discountSetting?.MinimumAmountThreshold ?? 5000,
                DiscountedPrice = discountedPrice,
                IsDiscountApplicable = isDiscountApplicable
            };

            return View(viewModel);
        }
    }
}
