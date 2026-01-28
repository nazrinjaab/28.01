using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaroTime.Application.DTOs.Coffees
{
    public record AnswerCoffeeDto(
        [Required]
        long CoffeeId,
        [Required]
        string Answer
        );
    
}
