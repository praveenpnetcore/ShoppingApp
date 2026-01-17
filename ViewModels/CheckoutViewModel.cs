using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(200)]
        [Display(Name = "Full Name")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(15)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        [StringLength(500)]
        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is required")]
        [StringLength(100)]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "PIN Code is required")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "PIN Code must be 6 digits")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "PIN Code must be 6 digits")]
        [Display(Name = "PIN Code")]
        public string PinCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a payment method")]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; } = string.Empty;

        // Order Summary
        public CartSummaryViewModel? CartSummary { get; set; }
    }
}
