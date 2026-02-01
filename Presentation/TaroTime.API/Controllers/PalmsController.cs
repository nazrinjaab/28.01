using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaroTime.Application.DTOs.Palm;
using TaroTime.Application.DTOs.Tarots;
using TaroTime.Application.Interfaces.Services;
using TaroTime.Domain.Entities;
using TaroTime.Persistence.Implementations.Services;

namespace TaroTime.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PalmsController : ControllerBase
    {
        private readonly IPalmService _palmService;
        private readonly UserManager<AppUser> _userManager;

        public PalmsController(IPalmService palmService,
            UserManager<AppUser> userManager)
        {
            _palmService = palmService;
            _userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreatePalmRequestDto dto)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();
            await _palmService.CreateRequestAsync(userId, dto);
            return Ok("Palm reading request created successfully.");
        }


        [HttpPost("accept/{palmId}")]
        public async Task<IActionResult> Accept(long palmId)
        {
            var readerId = _userManager.GetUserId(User);
            if (readerId == null) return Unauthorized();
            await _palmService.AcceptAsync(palmId, readerId);
            return Ok("Palm reading accepted.");
        }

        [HttpPost("answer")]
        public async Task<IActionResult> Answer([FromBody] AnswerPalmDto dto)
        {
            var readerId = _userManager.GetUserId(User);
            if (readerId == null) return Unauthorized();
            await _palmService.AnswerAsync(readerId, dto);
            return Ok("Palm reading answered.");
        }
       
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            if (!User.Identity?.IsAuthenticated ?? true)
                return Unauthorized(); 

            var result = await _palmService.GetAllAsync();
            return Ok(result);
        }

        
        [HttpGet("my-palm")]
        public async Task<IActionResult> GetMy()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            var result = await _palmService.GetByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("get-my-requests")]
        public async Task<IActionResult> GetMyRequests()
        {
            var readerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(readerId))
                return Unauthorized();
            var result = await _palmService.GetByReaderIdAsync(readerId);
            return Ok(result);
        }
        //[HttpGet("get-my-requests")]
        //public async Task<IActionResult> GetMyRequests()
        //{
        //    var readerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    if (string.IsNullOrEmpty(readerId))
        //        return Unauthorized();
        //    var result = await _palmService.GetByReaderIdAsync(readerId);
        //    return Ok(result);
        //}
    }
}
