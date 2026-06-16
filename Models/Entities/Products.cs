using System.ComponentModel.DataAnnotations;

namespace pratice_aegona_v2.Models.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}