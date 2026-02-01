using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Palm;
using TaroTime.Domain.Entities;

namespace TaroTime.Application.Validators.Palm
{
    internal class PalmReadingDtoValidator:AbstractValidator<PalmReadingDto>
    {
        public PalmReadingDtoValidator()
        {
            RuleFor(x => x.ReaderId)
              .NotEmpty();
              
            RuleFor(x => x.Answer)
            .NotEmpty().WithMessage("Cavab mətni boş ola bilməz.");

            RuleFor(x => x.Question)
               .NotEmpty();

        }
    }
}
