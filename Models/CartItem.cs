using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingApp.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SessionId { get; set; } = string.Empty;

        public string? UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.Now;
    }
}
