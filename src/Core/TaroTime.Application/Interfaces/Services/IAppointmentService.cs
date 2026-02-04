using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Appointments;

namespace TaroTime.Application.Interfaces.Services
{
    public interface IAppointmentService
    {
        Task CreateAsync(string userId, CreateAppointmentDto dto);
        Task CancelAsync(string userId, CancelAppointmentDto dto);
        Task<List<GetAppointmentItemDto>> GetAllAsync();
        Task<List<GetAppointmentItemDto>> GetPendingAsync();
        Task<List<GetAppointmentItemDto>> GetMyAppointmentsAsync();
        Task<List<ExpertScheduleDto>> GetExpertWeeklyScheduleAsync(string expertId);
        Task AcceptAsync(long appointmentId, string expertId);
    }
}
