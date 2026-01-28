using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Enums;

namespace TaroTime.Application.DTOs.Appointments
{
    public record CreateAppointmentDto(
     string ExpertId,
     DateTime StartTime,
     DateTime EndTime,
     MeetingType MeetingType
    );
    
}
