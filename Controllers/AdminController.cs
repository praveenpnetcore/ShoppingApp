using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Models;
using ShoppingApp.Services;

namespace ShoppingApp.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IDiscountService _discountService;
        private readonly IOrderService _orderService;

        public AdminController(
            IProductService productService,
            ICategoryService categoryService,
            IDiscountService discountService,
            IOrderService orderService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _discountService = discountService;
            _orderService = orderService;
        }

        [Route("")]
        [Route("Dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var products = await _productService.GetAllProductsAsync();
            var orders = await _orderService.GetAllOrdersAsync();
            var categories = await _categoryService.GetAllCategoriesAsync();

            ViewBag.TotalProducts = products.Count();
            ViewBag.TotalOrders = orders.Count();
            ViewBag.TotalCategories = categories.Count();
            ViewBag.TotalRevenue = orders.Sum(o => o.TotalAmount);
            ViewBag.RecentOrders = orders.Take(5);

            return View();
        }

        #region Products Management

        [Route("Products")]
        public async Task<IActionResult> Products()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        [Route("Products/Create")]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.Categories = await _categoryService.GetActiveCategoriesAsync();
            return View(new Product());
        }

        [HttpPost]
        [Route("Products/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateProductAsync(product);
                TempData["Success"] = "Product created successfully!";
                return RedirectToAction(nameof(Products));
            }

            ViewBag.Categories = await _categoryService.GetActiveCategoriesAsync();
            return View(product);
        }

        [Route("Products/Edit/{id}")]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _categoryService.GetActiveCategoriesAsync();
            return View(product);
        }

        [HttpPost]
        [Route("Products/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(product);
                TempData["Success"] = "Product updated successfully!";
                return RedirectToAction(nameof(Products));
            }

            ViewBag.Categories = await _categoryService.GetActiveCategoriesAsync();
            return View(product);
        }

        [HttpPost]
        [Route("Products/Delete/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            TempData["Success"] = "Product deleted successfully!";
            return RedirectToAction(nameof(Products));
        }

        #endregion

        #region Categories Management

        [Route("Categories")]
        public async Task<IActionResult> Categories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        [Route("Categories/Create")]
        public IActionResult CreateCategory()
        {
            return View(new Category());
        }

        [HttpPost]
        [Route("Categories/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategoryAsync(category);
                TempData["Success"] = "Category created successfully!";
                return RedirectToAction(nameof(Categories));
            }

            return View(category);
        }

        [Route("Categories/Edit/{id}")]
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [Route("Categories/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(category);
                TempData["Success"] = "Category updated successfully!";
                return RedirectToAction(nameof(Categories));
            }

            return View(category);
        }

        #endregion

        #region Discount Settings

        [Route("Discount")]
        public async Task<IActionResult> DiscountSettings()
        {
            var settings = await _discountService.GetAllDiscountSettingsAsync();
            return View(settings);
        }

        [Route("Discount/Edit/{id}")]
        public async Task<IActionResult> EditDiscount(int id)
        {
            var setting = await _discountService.GetDiscountSettingByIdAsync(id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        [HttpPost]
        [Route("Discount/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDiscount(int id, DiscountSetting setting)
        {
            if (id != setting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _discountService.UpdateDiscountSettingAsync(setting);
                TempData["Success"] = "Discount settings updated successfully!";
                return RedirectToAction(nameof(DiscountSettings));
            }

            return View(setting);
        }

        [Route("Discount/Create")]
        public IActionResult CreateDiscount()
        {
            return View(new DiscountSetting());
        }

        [HttpPost]
        [Route("Discount/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDiscount(DiscountSetting setting)
        {
            if (ModelState.IsValid)
            {
                await _discountService.CreateDiscountSettingAsync(setting);
                TempData["Success"] = "Discount setting created successfully!";
                return RedirectToAction(nameof(DiscountSettings));
            }

            return View(setting);
        }

        #endregion

        #region Orders Management

        [Route("Orders")]
        public async Task<IActionResult> Orders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        [Route("Orders/{id}")]
        public async Task<IActionResult> OrderDetails(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        [Route("Orders/UpdateStatus")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, OrderStatus status)
        {
            await _orderService.UpdateOrderStatusAsync(orderId, status);
            TempData["Success"] = "Order status updated successfully!";
            return RedirectToAction(nameof(OrderDetails), new { id = orderId });
        }

        #endregion
    }
}
