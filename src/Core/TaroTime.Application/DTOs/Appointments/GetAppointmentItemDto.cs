using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Enums;

namespace TaroTime.Application.DTOs.Appointments
{
    public record GetAppointmentItemDto(
     long Id,
     string UserName,
     string ExpertName,
     DateTime StartTime,
     DateTime EndTime,
     AppointmentStatus Status,
     MeetingType MeetingType,
     string? MeetingLink
    );
    
    
}
