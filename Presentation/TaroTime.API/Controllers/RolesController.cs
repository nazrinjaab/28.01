using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaroTime.Application.DTOs.Accounts;
using TaroTime.Application.Interfaces.Services;
using TaroTime.Domain.Entities;
using TaroTime.Domain.Enums;

namespace TaroTime.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly UserManager<AppUser> _userManager;

        public RolesController(IRoleService roleService, UserManager<AppUser> userManager)
        {
            _roleService = roleService;
            _userManager = userManager;
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole(string userId, UserRole role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("User not found");

            await _roleService.AssignRoleAsync(user, role);
            return Ok($"Role {role} assigned to {user.UserName}");
        }

        [HttpGet("{userId}/roles")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("User not found");

            var roles = await _roleService.GetUserRolesAsync(user);
            return Ok(roles);
        }
    }

}
