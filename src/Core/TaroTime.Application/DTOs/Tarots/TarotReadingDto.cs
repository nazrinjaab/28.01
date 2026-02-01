using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaroTime.Application.DTOs.Tarots
{
    public record TarotReadingDto(
        long Id,
        string Question,
        string? Answer,
        string UserId
        );
    
}
