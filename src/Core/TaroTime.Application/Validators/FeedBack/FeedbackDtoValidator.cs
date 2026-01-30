using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.FeedBack;

namespace TaroTime.Application.Validators.FeedBack
{
    public class FeedbackDtoValidator:AbstractValidator<FeedbackDto>
    {
        public FeedbackDtoValidator()
        {
            RuleFor(x => x.Email)
              .NotEmpty()
              .MinimumLength(4)
              .MaximumLength(256)
              .Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

            RuleFor(x => x.UserName)
               .NotEmpty()
               .MinimumLength(4)
               .MaximumLength(256)
               .Matches(@"^[A-Za-z-._@+]*$");

            RuleFor(x => x.Type)
           .IsInEnum().WithMessage("Tip yalnız Təklif və ya Şikayət ola bilər.");

            RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Mesaj mətni boş ola bilməz.");

            RuleFor(x => x.CreatedAt)
            .NotEmpty().WithMessage("CreatedAt boş ola bilməz.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("CreatedAt gələcək tarix ola bilməz.");


        }
    }
}
