
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaroTime.Application;
using TaroTime.Application.DTOs.AppUsers;
using TaroTime.Application.Interfaces.Services;
using TaroTime.Domain.Entities;

namespace TaroTime.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthenticationService _service;
        private readonly UserManager<AppUser> _userManager;

        public AccountsController(IAuthenticationService service,
            UserManager<AppUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterDto userDto)
        {
            await _service.RegisterAsync(userDto);
            return Created();

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDto userDto)
        {
          return Ok(await _service.LoginAsync(userDto));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromForm]ForgotPasswordDto userdto)
        {
            var token = await _service.ForgotPasswordAsync(userdto);
            return Ok(new
            {
                ResetToken = token
            });
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromForm]ResetPasswordDto userdto)
        {
           await _service.ResetPasswordAsync(userdto);
            return Ok("Password reset successfully");
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            if (!User.Identity?.IsAuthenticated ?? true)
                return Unauthorized();
            await _service.LogoutAsync();
            return Ok("You have been logged out successfully.");
        }

    }
}
