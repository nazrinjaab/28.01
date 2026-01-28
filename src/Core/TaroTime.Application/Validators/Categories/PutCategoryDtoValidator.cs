using FluentValidation;
using TaroTime.Application.DTOs;

namespace TaroTime.Application.Validators
{
    public class PutCategoryDtoValidator:AbstractValidator<PutCategoryDto>
    {
        private const int MAX_LIMIT = 150;
        private const int MIN_LIMIT = 2;
        public PutCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("name is required")
                .MaximumLength(MAX_LIMIT)
                    .WithMessage("150 olar max")
                .MinimumLength(MIN_LIMIT)
                    .WithMessage("min 2 olar")
                .Matches(@"^[A-Za-z0-9\s]*$");
        }
    }
}
