using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Appointments;

namespace TaroTime.Application.Validators.Appointment
{
    public class CancelAppointmentDtoValidator:AbstractValidator<CancelAppointmentDto>
    {
        public CancelAppointmentDtoValidator()
        {
            RuleFor(x => x.AppointmentId)
               .NotEmpty()
               .GreaterThan(0);
        }
    }
}
