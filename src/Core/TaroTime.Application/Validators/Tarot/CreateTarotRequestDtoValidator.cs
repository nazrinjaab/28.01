using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Tarots;

namespace TaroTime.Application.Validators.Tarot
{
    public class CreateTarotRequestDtoValidator:AbstractValidator<CreateTarotRequestDto>
    {
        public CreateTarotRequestDtoValidator()
        {
            RuleFor(x => x.Question)
           .NotEmpty().WithMessage("Sual mətni boş ola bilməz.");
        }
    }
}
