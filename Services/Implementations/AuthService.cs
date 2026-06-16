using Microsoft.EntityFrameworkCore;
using pratice_aegona_v2.Data;
using pratice_aegona_v2.Models.Entities;
using pratice_aegona_v2.Models.ViewModels;
using pratice_aegona_v2.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace pratice_aegona_v2.Services.Implementations
{
    public class AuthService(AppDbContext context, ITokenService tokenService) : IAuthService
    {
        private readonly AppDbContext _context = context;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<bool> Register(RegisterViewModel model)
        {
            var defaultRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "User");
            if (defaultRole == null)
            {
                throw new Exception("Hệ thống chưa thiết lập quyền mặc định 'User' trong cơ sở dữ liệu.");
            }

            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                UserName = model.UserName,
                PasswordHash = HashPassword(model.Password),
                RoleId = defaultRole.Id // Gán quyền mặc định là User
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(string accessToken, string refreshToken)> Login(LoginViewModel model)
        {
            // Bắt buộc dùng Include để nạp kèm thông tin bảng Role phục vụ sinh JWT Claim
            var user = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null) return (null, null);

            if (user.PasswordHash != HashPassword(model.Password))
                return (null, null);

            var accessToken = _tokenService.CreateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            refreshToken.UserId = user.Id;

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return (accessToken, refreshToken.Token);
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}