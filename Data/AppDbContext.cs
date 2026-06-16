using Microsoft.EntityFrameworkCore;
using pratice_aegona_v2.Models.Entities;

namespace pratice_aegona_v2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}