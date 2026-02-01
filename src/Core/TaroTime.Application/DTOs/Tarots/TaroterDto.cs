using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Enums;

namespace TaroTime.Application.DTOs.Tarots
{
    public record TaroterDto(
        long Id,
       string Question,
       string UserId,
       TarotStatus Status,
       DateTime CreatedAt,
       string? Answer
        );
   
}
