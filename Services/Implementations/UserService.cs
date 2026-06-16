using Microsoft.EntityFrameworkCore;
using pratice_aegona_v2.Data;
using pratice_aegona_v2.Models.Entities;
using pratice_aegona_v2.Models.ViewModels;
using pratice_aegona_v2.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace pratice_aegona_v2.Services.Implementations
{
    public class UserService(AppDbContext context) : IUserService
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<UserResponseViewModel>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Select(u => new UserResponseViewModel
                {
                    Id = u.Id,
                    Username = u.UserName ?? string.Empty,
                    Email = u.Email ?? string.Empty,
                    Role = u.Role != null ? u.Role.Name : string.Empty,
                    IsActive = true
                })
                .ToListAsync();
        }

        public async Task<UserResponseViewModel?> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return null;

            return new UserResponseViewModel
            {
                Id = user.Id,
                Username = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Role = user.Role != null ? user.Role.Name : string.Empty,
                IsActive = true
            };
        }

        public async Task<UserResponseViewModel> CreateUserAsync(CreateUserViewModel model)
        {
            var isExist = await _context.Users.AnyAsync(u => u.UserName == model.Username || u.Email == model.Email);
            if (isExist) throw new Exception("Username hoặc Email đã tồn tại trong hệ thống.");

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == model.Role);
            if (role == null) throw new Exception("Quyền (Role) được chỉ định không tồn tại.");

            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = model.Username,
                Email = model.Email,
                PasswordHash = HashPassword(model.Password),
                RoleId = role.Id,
                Role = role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserResponseViewModel
            {
                Id = user.Id,
                Username = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Role = role.Name,
                IsActive = true
            };
        }

        public async Task<UserResponseViewModel?> UpdateUserAsync(Guid id, UpdateUserViewModel model)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return null;

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == model.Role);
            if (role == null) throw new Exception("Quyền (Role) được chỉ định không tồn tại.");

            user.Email = model.Email;
            user.RoleId = role.Id;
            user.Role = role;

            await _context.SaveChangesAsync();

            return new UserResponseViewModel
            {
                Id = user.Id,
                Username = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Role = role.Name,
                IsActive = true
            };
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}