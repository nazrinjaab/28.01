using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaroTime.Application.DTOs.Horoscope;
using TaroTime.Application.DTOs.Tarots;
using TaroTime.Application.Interfaces.Services.Horoscope;
using TaroTime.Domain.Entities;

namespace TaroTime.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HoroscopesController : ControllerBase
    {
       
        
            private readonly ICompatibilityZodiacService _service;
        private readonly UserManager<AppUser> _userManager;

        public HoroscopesController(ICompatibilityZodiacService service,
                 UserManager<AppUser> userManager)
            {
                _service = service;
            _userManager = userManager;
        }

            
            [HttpPost("create")]
            public async Task<IActionResult> CreateCompatibility( [FromBody] CreateCompatibilityDto dto)
            {
                var userId = _userManager.GetUserId(User);
                await _service.CreateCompatibilityAsync(userId, dto);
                return Ok("Compatibility request created successfully.");
            }


           [HttpPost("accept/{compatibilityId}")]
            public async Task<IActionResult> Accept(long compatibilityId)
            {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            await _service.AcceptAsync(compatibilityId, userId);
                return Ok("Compatibility request accepted.");
            }


        [HttpPost("answer")]
        public async Task<IActionResult> Answer([FromForm] AnswerCompatibilityDto dto)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            await _service.AnswerAsync(userId, dto);
            return Ok("Tarot answered successfully");
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            if (!User.Identity?.IsAuthenticated ?? true)
                return Unauthorized();
            var result = await _service.GetAllAsync();
            return Ok(result);
        }


        [HttpGet("my-palm")]
        public async Task<IActionResult> GetMy()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            var result = await _service.GetByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("get-my-requests")]
        public async Task<IActionResult> GetMyRequests()
        {
            var expertId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(expertId))
                return Unauthorized();
            var result = await _service.GetByReaderIdAsync(expertId);
            return Ok(result);
        }




    }
}


