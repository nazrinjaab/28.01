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
    public class GetAppointmentItemDtoValidator:AbstractValidator<GetAppointmentItemDto>
    {
        private const int MAX_LIMIT = 150;
        private const int MIN_LIMIT = 2;
        public GetAppointmentItemDtoValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty()
               .GreaterThan(0);

            RuleFor(x => x.UserName)
             .NotEmpty()
             .MinimumLength(4)
             .MaximumLength(256)
             .Matches(@"^[A-Za-z-._@+]*$");

            RuleFor(x => x.ExpertName)
               .NotEmpty()
                   .WithMessage("name is required")
                   .MaximumLength(MAX_LIMIT)
                   .WithMessage("50 olar max")
               .MinimumLength(MIN_LIMIT)
                   .WithMessage("min 2 olar")
               .Matches(@"^[A-Za-z]*$");

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

            RuleFor(x => x.MeetingLink)
                .NotEmpty()
                .When(x => x.MeetingType == MeetingType.Online)
                .WithMessage("Online görüş üçün MeetingLink boş ola bilməz.");

            RuleFor(x => x.MeetingLink)
                .Empty()
                .When(x => x.MeetingType == MeetingType.Offline)
                .WithMessage("Offline görüş üçün MeetingLink olmamalıdır.");


        }
        private bool BeFutureDate(DateTime startTime)
        {
            return startTime > DateTime.UtcNow;
        }
    }
}
