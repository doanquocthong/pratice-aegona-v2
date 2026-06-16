using System.ComponentModel.DataAnnotations;

namespace pratice_aegona_v2.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}