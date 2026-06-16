using pratice_aegona_v2.Models.ViewModels;

namespace pratice_aegona_v2.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> Register(RegisterViewModel model);
        Task<(string accessToken, string refreshToken)> Login(LoginViewModel model);
    }
}