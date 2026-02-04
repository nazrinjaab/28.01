using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaroTime.Application.DTOs.Coffees;
using TaroTime.Application.Interfaces.Services;
using TaroTime.Domain.Entities;

namespace TaroTime.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoffeesController : ControllerBase
    {
        private readonly ICoffeeService _coffeeService;
        private readonly UserManager<AppUser> _userManager;

        public CoffeesController(ICoffeeService coffeeService,
             UserManager<AppUser> userManager)
        {
            _coffeeService = coffeeService;
            _userManager = userManager;
        }

      
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreateCoffeeRequestDto dto)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();
            await _coffeeService.CreateRequestAsync(userId, dto);
            return Ok("Coffee reading request created successfully.");
        }

      
        [HttpPost("accept/{coffeeId}")]
        public async Task<IActionResult> Accept(long coffeeId)
        {
            var readerId = _userManager.GetUserId(User);
            if (readerId == null) return Unauthorized();
            await _coffeeService.AcceptAsync(coffeeId, readerId);
            return Ok("Coffee reading accepted.");
        }

      
        [HttpPost("answer")]
        public async Task<IActionResult> Answer([FromBody] AnswerCoffeeDto dto)
        {
            var readerId = _userManager.GetUserId(User);
            if (readerId == null) return Unauthorized();
            await _coffeeService.AnswerAsync(readerId, dto);
            return Ok("Coffee reading answered.");

        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            if (!User.Identity?.IsAuthenticated ?? true)
                return Unauthorized();

            var result = await _coffeeService.GetAllAsync();
            return Ok(result);
        }


        [HttpGet("my-palm")]
        public async Task<IActionResult> GetMy()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            var result = await _coffeeService.GetByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("get-my-requests")]
        public async Task<IActionResult> GetMyRequests()
        {
            var readerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(readerId))
                return Unauthorized();
            var result = await _coffeeService.GetByReaderIdAsync(readerId);
            return Ok(result);
        }
    }
}
