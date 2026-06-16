using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pratice_aegona_v2.Models.ViewModels;
using pratice_aegona_v2.Services.Interfaces;

namespace pratice_aegona_v2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound(new { message = "Không tìm thấy người dùng" });
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var newUser = await _userService.CreateUserAsync(model);
                return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedUser = await _userService.UpdateUserAsync(id, model);
            if (updatedUser == null) return NotFound(new { message = "Không tìm thấy người dùng để cập nhật" });

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result) return NotFound(new { message = "Không tìm thấy người dùng để xóa" });

            return Ok(new { message = "Xóa người dùng thành công" });
        }
    }
}