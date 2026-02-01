using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaroTime.Application.DTOs.FeedBack;
using TaroTime.Application.Interfaces.Services;

namespace TaroTime.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FeedBacksController : ControllerBase
    {
        private readonly IFeedbackService _service;

        public FeedBacksController(IFeedbackService service)
        {
            _service = service;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromForm] FeedbackDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            await _service.SubmitAsync(dto);
            return Ok("Rəyiniz uğurla göndərildi.");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var feedbacks = await _service.GetAllAsync();
            return Ok(feedbacks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var feedback = await _service.GetByIdAsync(id);
            if (feedback == null) return NotFound();
            return Ok(feedback);
        }

    }
}
