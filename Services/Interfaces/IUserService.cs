using pratice_aegona_v2.Models.ViewModels;

namespace pratice_aegona_v2.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseViewModel>> GetAllUsersAsync();
        Task<UserResponseViewModel?> GetUserByIdAsync(Guid id);
        Task<UserResponseViewModel> CreateUserAsync(CreateUserViewModel model);
        Task<UserResponseViewModel?> UpdateUserAsync(Guid id, UpdateUserViewModel model);
        Task<bool> DeleteUserAsync(Guid id);
    }
}