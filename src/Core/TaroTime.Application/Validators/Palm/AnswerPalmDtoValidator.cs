using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Palm;

namespace TaroTime.Application.Validators.Palm
{
    public class AnswerPalmDtoValidator:AbstractValidator<AnswerPalmDto>
    {
        public AnswerPalmDtoValidator()
        {
            RuleFor(x => x.PalmId)
              .NotEmpty()
              .GreaterThan(0);

            RuleFor(x => x.Answer)
            .NotEmpty().WithMessage("Cavab mətni boş ola bilməz.");

        }
    }
}
