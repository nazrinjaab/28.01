using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Appointments;
using TaroTime.Application.Interfaces.Repositories;
using TaroTime.Application.Interfaces.Services;
using TaroTime.Domain.Entities;
using TaroTime.Domain.Enums;

namespace TaroTime.Persistence.Implementations.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentService(IAppointmentRepository repository)
        {
            _repository = repository;
        }

       
        public async Task CreateAsync(string userId, CreateAppointmentDto dto)
        {
            var appointment = new Appointment
            {
                UserId = userId,
                ExpertId = dto.ExpertId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                MeetingType = dto.MeetingType,
                Status = AppointmentStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

           
            if (dto.MeetingType == MeetingType.Online)
            {
                appointment.MeetingLink = $"https://meet.example.com/{Guid.NewGuid()}";
            }

            _repository.Add(appointment);
            await _repository.SaveChangesAsync();
        }

       
        public async Task CancelAsync(string userId, CancelAppointmentDto dto)
        {
            var appointment = await _repository.GetByIdAsync(dto.AppointmentId)
                ?? throw new Exception("Appointment not found");

            if (appointment.UserId != userId)
                throw new Exception("You cannot cancel someone else's appointment");

            appointment.Status = AppointmentStatus.Cancelled;
            appointment.UpdatedAt = DateTime.UtcNow;

            _repository.Update(appointment);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<GetAppointmentItemDto>> GetAllAsync()
        {
            var appointments = await _repository
         .GetAll(
         sort: a => a.StartTime,
         includes: [nameof(Appointment.User), nameof(Appointment.Expert)]
         )
        .ToListAsync();

            var result = appointments.Select(x => new GetAppointmentItemDto(
                x.Id,
                x.User?.UserName ?? "unknown",
                x.Expert?.UserName ?? "unknown",
                x.StartTime,
                x.EndTime,
                x.Status,
                x.MeetingType,
                x.MeetingLink
            )).ToList();

            return result;
        }
        public async Task<List<GetAppointmentItemDto>> GetPendingAsync()
        {
            var appointments = await _repository
                .GetAll(
                    
                    sort: a => a.StartTime,
                 
                    includes:  [nameof(Appointment.User), nameof(Appointment.Expert)]
                )
                .ToListAsync();

            return appointments.Select(a => new GetAppointmentItemDto(
                a.Id,
                a.User?.UserName ?? "unknown",
                a.Expert?.UserName ?? "unknown",
                a.StartTime,
                a.EndTime,
                a.Status,
                a.MeetingType,
                a.MeetingLink
            )).ToList();
        }
        public async Task<List<GetAppointmentItemDto>> GetMyAppointmentsAsync()
        {
            var appointments = await _repository
                .GetAll(
                    sort: a => a.StartTime,
                    includes:  [ nameof(Appointment.User), nameof(Appointment.Expert) ]
                )
                .ToListAsync();

            var result = appointments.Select(a => new GetAppointmentItemDto(
                a.Id,
                a.User?.UserName ?? "unknown",
                a.Expert?.UserName ?? "unknown",
                a.StartTime,
                a.EndTime,
                a.Status,
                a.MeetingType,
                a.MeetingLink
            )).ToList();

            return result;
        }
        public async Task<List<ExpertScheduleDto>> GetExpertWeeklyScheduleAsync(string expertId)
        {
            var appointments = await _repository
                .GetAll(
                    sort: a => a.StartTime
                )
                .ToListAsync();

            var freeSlots = new List<ExpertScheduleDto>();
            var workDayStartHour = 9;
            var workDayEndHour = 18;

            for (int i = 0; i < 7; i++) 
            {
                var day = DateTime.Today.AddDays(i);
                var workDayStart = day.AddHours(workDayStartHour);
                var workDayEnd = day.AddHours(workDayEndHour);

                var dayAppointments = appointments
                    .Where(a => a.StartTime.Date == day.Date)
                    .OrderBy(a => a.StartTime)
                    .ToList();

                var current = workDayStart;

                foreach (var appointment in dayAppointments)
                {
                    if (current < appointment.StartTime)
                    {
                        freeSlots.Add(new ExpertScheduleDto(
                            current,
                            appointment.StartTime
                        ));
                    }
                    current = appointment.EndTime;
                }

                if (current < workDayEnd)
                {
                    freeSlots.Add(new ExpertScheduleDto(current, workDayEnd));
                }
            }

            return freeSlots;
        }
    }
}
