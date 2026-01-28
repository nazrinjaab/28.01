using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaroTime.Application.DTOs.Coffees
{
    public record CreateCoffeeRequestDto(
        [Required]
        string Question,
        [Required]
        IFormFile CupImage 
        );
    
}
