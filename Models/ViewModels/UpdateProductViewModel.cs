using System.ComponentModel.DataAnnotations;

namespace pratice_aegona_v2.Models.ViewModels
{
    public class UpdateProductViewModel
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Giá sản phẩm phải lớn hơn hoặc bằng 0")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng kho phải lớn hơn hoặc bằng 0")]
        public int StockQuantity { get; set; }

        public string? ImageUrl { get; set; }
    }
}