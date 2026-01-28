using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaroTime.Application.DTOs.Appointments;
using TaroTime.Application.Interfaces.Services;

namespace TaroTime.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
       
            private readonly IAppointmentService _appointmentService;

            public AppointmentsController(IAppointmentService appointmentService)
            {
                _appointmentService = appointmentService;
            }

         
            [HttpGet("all")]
            public async Task<IActionResult> GetAll()
            {
                var appointments = await _appointmentService.GetAllAsync();
                return Ok(appointments);
            }

           
            [HttpGet("pending")]
            public async Task<IActionResult> GetPending()
            {
                var appointments = await _appointmentService.GetPendingAsync();
                return Ok(appointments);
            }


        
            [HttpGet("my")]
            public async Task<IActionResult> GetMyAppointments()
            {
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

            await _appointmentService.CreateAsync(userId, dto);
            return Ok("Appointment created successfully.");
        }



        [HttpPost("cancel")]
            public async Task<IActionResult> Cancel([FromBody] CancelAppointmentDto dto)
            {
                var userId = User.Identity?.Name ?? "test-user";
                await _appointmentService.CancelAsync(userId, dto);
                return Ok("Appointment cancelled.");
            }
    }
}

