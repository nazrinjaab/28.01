using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaroTime.Application.DTOs.Appointments;
using TaroTime.Application.Interfaces.Services;
using TaroTime.Domain.Entities;
using TaroTime.Persistence.Contexts.Migrations;

namespace TaroTime.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
       
            private readonly IAppointmentService _appointmentService;
        private readonly UserManager<AppUser> _userManager;

        public AppointmentsController(IAppointmentService appointmentService,
                UserManager<AppUser> userManager)
            {
                _appointmentService = appointmentService;
            _userManager = userManager;
        }

         
            [HttpGet("all")]
            public async Task<IActionResult> GetAll()
            {
            if (!User.Identity?.IsAuthenticated ?? true)
                return Unauthorized();

            var appointments = await _appointmentService.GetAllAsync();
                return Ok(appointments);
            }

           
            [HttpGet("pending")]
            public async Task<IActionResult> GetPending()
            {

            var expertId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (string.IsNullOrEmpty(expertId))
            //    return Unauthorized();


            var appointments = await _appointmentService.GetPendingAsync();
                return Ok(appointments);
            }


        [HttpPost("accept/{appointmentId}")]
        public async Task<IActionResult> Accept(long appointmentId)
        {
            var expertId = _userManager.GetUserId(User);
            if (expertId == null) return Unauthorized();
            await _appointmentService.AcceptAsync(appointmentId, expertId);
            return Ok("Palm reading accepted.");
        }

     

        [HttpGet("my")]
            public async Task<IActionResult> GetMyAppointments()
            {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var appointments = await _appointmentService.GetMyAppointmentsAsync();
            return Ok(appointments);
            }

        [HttpGet("expert/{expertId}/weekly-schedule")]
        public async Task<IActionResult> GetExpertWeeklySchedule(string expertId)
        {

            var schedule = await _appointmentService.GetExpertWeeklyScheduleAsync(expertId);
            return Ok(schedule);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();



            await _appointmentService.CreateAsync(userId, dto);
            return Ok("Appointment created successfully.");
        }



        [HttpPost("cancel")]
            public async Task<IActionResult> Cancel([FromBody] CancelAppointmentDto dto)
            {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
             await _appointmentService.CancelAsync(userId, dto);
                return Ok("Appointment cancelled.");
            }
    }
}

