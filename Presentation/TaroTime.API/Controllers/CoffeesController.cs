using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaroTime.Application.DTOs.Coffees;
using TaroTime.Application.Interfaces.Services;

namespace TaroTime.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoffeesController : ControllerBase
    {
        private readonly ICoffeeService _coffeeService;

        public CoffeesController(ICoffeeService coffeeService)
        {
            _coffeeService = coffeeService;
        }

      
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreateCoffeeRequestDto dto)
        {
            var userId = User.Identity?.Name ?? "test-user"; 
            await _coffeeService.CreateRequestAsync(userId, dto);
            return Ok("Coffee reading request created successfully.");
        }

      
        [HttpPost("accept/{coffeeId}")]
        public async Task<IActionResult> Accept(long coffeeId)
        {
            var readerId = User.Identity?.Name ?? "falci-1";
            await _coffeeService.AcceptAsync(coffeeId, readerId);
            return Ok("Coffee reading accepted.");
        }

      
        [HttpPost("answer")]
        public async Task<IActionResult> Answer([FromBody] AnswerCoffeeDto dto)
        {
            var readerId = User.Identity?.Name ?? "falci-1";
            await _coffeeService.AnswerAsync(readerId, dto);
            return Ok("Coffee reading answered.");

        }
    }
}
