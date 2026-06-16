using System.ComponentModel.DataAnnotations;

namespace pratice_aegona_v2.Models.ViewModels
{
    public class UpdateUserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}