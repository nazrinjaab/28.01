using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Appointments;

namespace TaroTime.Application.Validators.Appointment
{
    public class ExpertScheduleDtoValidator:AbstractValidator<ExpertScheduleDto>
    {
        public ExpertScheduleDtoValidator()
        {
            RuleFor(x => x.FreeStart)
           .NotEmpty().WithMessage("FreeStart boş ola bilməz.")
           .Must(BeFutureDate).WithMessage("FreeStart keçmişdə ola bilməz.");

            RuleFor(x => x.FreeEnd)
                .NotEmpty().WithMessage("FreeEnd boş ola bilməz.")
                .GreaterThan(x => x.FreeStart)
                .WithMessage("FreeEnd FreeStart-dan sonra olmalıdır.");

            RuleFor(x => x)
                .Must(x => (x.FreeEnd - x.FreeStart).TotalMinutes >= 30)
                .WithMessage("Boş vaxt intervalı ən azı 30 dəqiqə olmalıdır.");
        }

        private bool BeFutureDate(DateTime date)
        {
            return date > DateTime.UtcNow;
        }

    }
}

