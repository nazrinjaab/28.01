using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Appointments;
using TaroTime.Domain.Enums;

namespace TaroTime.Application.Validators.Appointment
{
    public class CreateAppointmentDtoValidator:AbstractValidator<CreateAppointmentDto>
    {
        public CreateAppointmentDtoValidator()
        {
            RuleFor(x => x.ExpertId)
               .NotEmpty();

            RuleFor(x => x.StartTime)
           .NotEmpty().WithMessage("Başlama vaxtı boş ola bilməz.")
           .Must(BeFutureDate).WithMessage("Başlama vaxtı keçmişdə ola bilməz.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("Bitmə vaxtı boş ola bilməz.")
                .GreaterThan(x => x.StartTime)
                .WithMessage("Bitmə vaxtı başlama vaxtından sonra olmalıdır.");

            RuleFor(x => x)
                .Must(x => (x.EndTime - x.StartTime).TotalMinutes >= 15)
                .WithMessage("Görüş ən azı 15 dəqiqə olmalıdır.");

            RuleFor(x => x.MeetingType)
            .IsInEnum()
            .WithMessage("MeetingType yalnız Online və ya Offline ola bilər.");

        }
        private bool BeFutureDate(DateTime startTime)
        {
            return startTime > DateTime.UtcNow;
        }

    }
}
