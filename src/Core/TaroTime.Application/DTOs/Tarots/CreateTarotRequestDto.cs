using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaroTime.Application.DTOs.Tarots
{
    public record CreateTarotRequestDto(
        [Required]
        string Question
        );
    
}
