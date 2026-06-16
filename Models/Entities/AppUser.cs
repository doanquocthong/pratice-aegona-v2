using System.ComponentModel.DataAnnotations;
using System.Data;

namespace pratice_aegona_v2.Models.Entities
{
    public class AppUser
    {
        [Key]
        public Guid Id { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }

        // FK Role
        public Guid RoleId { get; set; }
        public Roles Role { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}