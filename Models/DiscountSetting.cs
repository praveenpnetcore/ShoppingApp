using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingApp.Models
{
    public class DiscountSetting
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Setting Name")]
        public string SettingName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Threshold must be 0 or greater")]
        [Display(Name = "Minimum Amount Threshold (₹)")]
        public decimal MinimumAmountThreshold { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        [Range(0, 100, ErrorMessage = "Discount percentage must be between 0 and 100")]
        [Display(Name = "Discount Percentage (%)")]
        public decimal DiscountPercentage { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
    }
}
