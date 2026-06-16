using System.ComponentModel.DataAnnotations;

namespace pratice_aegona_v2.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string? UserName { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}