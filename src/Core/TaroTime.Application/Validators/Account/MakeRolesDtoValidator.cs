using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Accounts;

namespace TaroTime.Application.Validators.Account
{
    public class MakeRolesDtoValidator:AbstractValidator<MakeRolesDto>
    {
        private const int MAX_LIMIT = 150;
        private const int MIN_LIMIT = 2;
        public MakeRolesDtoValidator()
        {
            RuleFor(x => x.UserId)
               .NotEmpty();

            RuleFor(x => x.RoleName)
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
