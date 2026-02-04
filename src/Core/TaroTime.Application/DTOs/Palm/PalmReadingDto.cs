using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Enums;

namespace TaroTime.Application.DTOs.Palm
{
    public record PalmReadingDto(
       long Id,
       string Question,
       string? Answer,
       string UserId,
        string UserName,
       string ReaderId,
       PalmStatus Status
        );
    
}
