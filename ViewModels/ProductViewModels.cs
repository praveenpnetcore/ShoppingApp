using ShoppingApp.Models;

namespace ShoppingApp.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> FeaturedProducts { get; set; } = new List<Product>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public decimal DiscountPercentage { get; set; }
        public decimal MinimumAmountForDiscount { get; set; }
    }

    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public int? SelectedCategoryId { get; set; }
        public string? SearchTerm { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal MinimumAmountForDiscount { get; set; }
    }

    public class ProductDetailViewModel
    {
        public Product Product { get; set; } = null!;
        public IEnumerable<Product> RelatedProducts { get; set; } = new List<Product>();
        public decimal DiscountPercentage { get; set; }
        public decimal MinimumAmountForDiscount { get; set; }
        public decimal DiscountedPrice { get; set; }
        public bool IsDiscountApplicable { get; set; }
    }
}
