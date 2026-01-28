using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.AppUsers;

namespace TaroTime.Application.Validators.AppUser
{
    public class ResetPasswordValidator:AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.UsernameOrEmail)
              .NotEmpty()
              .MinimumLength(4)
              .MaximumLength(256);

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(256);

            RuleFor(x => x)
               .Must(x => x.ConfirmPassword == x.NewPassword)
               .WithMessage("password and confirm password must be the same");
        }
    }
}
