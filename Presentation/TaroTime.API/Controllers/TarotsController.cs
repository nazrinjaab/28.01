using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaroTime.Application.DTOs.Tarots;
using TaroTime.Application.Interfaces.Services;
using TaroTime.Domain.Entities;

namespace TaroTime.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TarotsController : ControllerBase
    {
        private readonly ITarotService _tarotService;
        private readonly UserManager<AppUser> _userManager;

        public TarotsController(ITarotService tarotService, UserManager<AppUser> userManager)
        {
            _tarotService = tarotService;
            _userManager = userManager;
        }

       
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreateTarotRequestDto dto)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            await _tarotService.CreateRequestAsync(userId, dto);
            return Ok("Tarot request created successfully");
        }

        
        [HttpPost("accept/{tarotId}")]
        public async Task<IActionResult> Accept(long tarotId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            await _tarotService.AcceptAsync(tarotId, userId);
            return Ok("Tarot request accepted");
        }

        
        [HttpPost("answer")]
        public async Task<IActionResult> Answer([FromForm] AnswerTarotDto dto)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            await _tarotService.AnswerAsync(userId, dto);
            return Ok("Tarot answered successfully");
        }

    }
}
