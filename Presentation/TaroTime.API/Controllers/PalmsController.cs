using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaroTime.Application.DTOs.Palm;
using TaroTime.Application.Interfaces.Services;

namespace TaroTime.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PalmsController : ControllerBase
    {
        private readonly IPalmService _palmService;

        public PalmsController(IPalmService palmService)
        {
            _palmService = palmService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreatePalmRequestDto dto)
        {
            var userId = User.Identity?.Name ?? "test-user";
            await _palmService.CreateRequestAsync(userId, dto);
            return Ok("Palm reading request created successfully.");
        }

        [HttpPost("accept/{palmId}")]
        public async Task<IActionResult> Accept(long palmId)
        {
            var readerId = User.Identity?.Name ?? "falci-1";
            await _palmService.AcceptAsync(palmId, readerId);
            return Ok("Palm reading accepted.");
        }

        [HttpPost("answer")]
        public async Task<IActionResult> Answer([FromBody] AnswerPalmDto dto)
        {
            var readerId = User.Identity?.Name ?? "falci-1";
            await _palmService.AnswerAsync(readerId, dto);
            return Ok("Palm reading answered.");
        }

    }
}
