namespace ShoppingApp.ViewModels
{
    public class CartItemViewModel
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalPrice { get; set; }
        public bool IsDiscountApplicable { get; set; }
        public decimal DiscountPercentage { get; set; }
    }

    public class CartSummaryViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; } = new();
        public decimal SubTotal { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal MinimumAmountForDiscount { get; set; }
        public int TotalItems { get; set; }
    }
}
