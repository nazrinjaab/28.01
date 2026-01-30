using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.FeedBack;

namespace TaroTime.Application.Validators.FeedBack
{
    public class isLoginDtoValidator:AbstractValidator<isLoginDto>
    {
        public isLoginDtoValidator()
        {
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
        }
    }
}
