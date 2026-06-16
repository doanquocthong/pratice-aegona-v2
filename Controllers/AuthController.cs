using Microsoft.AspNetCore.Mvc;
using pratice_aegona_v2.Models.ViewModels;
using pratice_aegona_v2.Services.Interfaces;

namespace pratice_aegona_v2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.Login(model);

            if (result.accessToken == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Sai tài khoản hoặc mật khẩu"
                });
            }

            HttpContext.Response.Cookies.Append("access_token", result.accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            HttpContext.Response.Cookies.Append("refresh_token", result.refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok(new
            {
                success = true,
                message = "Đăng nhập thành công",
                data = new
                {
                    accessToken = result.accessToken,
                    refreshToken = result.refreshToken
                }
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _authService.Register(model);

                return Ok(new
                {
                    success = true,
                    message = "Đăng ký tài khoản thành công"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}