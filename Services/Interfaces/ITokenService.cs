using pratice_aegona_v2.Models.Entities;

namespace pratice_aegona_v2.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
        RefreshToken GenerateRefreshToken();
    }
}