using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Tarots;

namespace TaroTime.Application.Validators.Tarot
{
    public class AnswerTarotDtoValidator:AbstractValidator<AnswerTarotDto>
    {
        public AnswerTarotDtoValidator()
        {
            RuleFor(x => x.TarotId)
               .NotEmpty()
               .GreaterThan(0);

            RuleFor(x => x.Answer)
            .NotEmpty().WithMessage("Cavab mətni boş ola bilməz.");

        }
    }
}
