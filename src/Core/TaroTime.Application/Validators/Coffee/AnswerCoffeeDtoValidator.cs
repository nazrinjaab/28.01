using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Coffees;

namespace TaroTime.Application.Validators.Coffee
{
    public class AnswerCoffeeDtoValidator:AbstractValidator<AnswerCoffeeDto>
    {
        public AnswerCoffeeDtoValidator()
        {
            RuleFor(x => x.CoffeeId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Answer)
            .NotEmpty().WithMessage("Cavab mətni boş ola bilməz.");

        }
    }
}
