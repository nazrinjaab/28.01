using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Coffees;

namespace TaroTime.Application.Validators.Coffee
{
   public class CreateCoffeeRequestDtoValidator:AbstractValidator<CreateCoffeeRequestDto>
    {
        public CreateCoffeeRequestDtoValidator()
        {

            RuleFor(x => x.Question)
            .NotEmpty().WithMessage("Sual mətni boş ola bilməz.");

            RuleFor(x => x.CupImage)
          .NotNull().WithMessage("Şəkil yüklənməlidir.")
          .Must(file => file.Length > 0).WithMessage("Şəkil boş ola bilməz.")
          .Must(file => file.Length <= 2 * 1024 * 1024).WithMessage("Şəkil maksimum 2MB ola bilər.")
          .Must(file => IsValidImage(file)).WithMessage("Yalnız JPG və PNG formatları qəbul edilir.");
        }

        private bool IsValidImage(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(extension);
        }

    }
}
