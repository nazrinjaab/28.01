using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.AppUsers;

namespace TaroTime.Application.Validators.AppUser
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {

        private const int MAX_LIMIT = 50;
        private const int MIN_LIMIT = 2;


        public RegisterDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("name is required")
                    .MaximumLength(MAX_LIMIT)
                    .WithMessage("50 olar max")
                .MinimumLength(MIN_LIMIT)
                    .WithMessage("min 2 olar")
                .Matches(@"^[A-Za-z]*$");

            RuleFor(x => x.Surname)
               .NotEmpty()
                   .WithMessage("name is required")
                   .MaximumLength(MAX_LIMIT)
                   .WithMessage("50 olar max")
               .MinimumLength(MIN_LIMIT)
                   .WithMessage("min 2 olar")
               .Matches(@"^[A-Za-z]*$");

            RuleFor(x => x.Email)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(256)
                .Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

            RuleFor(x => x.Username)
               .NotEmpty()
               .MinimumLength(4)
               .MaximumLength(256)
               .Matches(@"^[A-Za-z-._@+]*$");


            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(256)
                .Matches(@"^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$")
                .WithMessage("Minimum 8 characters\r\nShould have at least one number\r\nShould have at least one upper case\r\nShould have at least one lower case\r\nShould have at least one special character");

            RuleFor(x => x)
               .Must(x => x.ConfirmPassword == x.Password)
               .WithMessage("password and confirm password must be the same");
        }
    }
}
