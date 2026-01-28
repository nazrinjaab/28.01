using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Products;

namespace TaroTime.Application.Validators.Products
{
    public class PutProductDtoValidator : AbstractValidator<PutProductDto>
    {
        private const int MAX_LIMIT = 150;
        private const int MIN_LIMIT = 2;
        public PutProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("name is required")
                .MaximumLength(MAX_LIMIT)
                    .WithMessage("150 olar max")
                .MinimumLength(MIN_LIMIT)
                    .WithMessage("min 2 olar")
                .Matches(@"^[A-Za-z0-9\s]*$");

            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.SKU)
                .NotEmpty().
                MaximumLength(10);

            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThan(0m)
                .LessThanOrEqualTo(999999.99m);

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.TagIds)
               .NotEmpty()
               .Must(ti => ti.Count > 0);

            RuleForEach(x => x.TagIds)
                .GreaterThan(0);


        }
    }
}
