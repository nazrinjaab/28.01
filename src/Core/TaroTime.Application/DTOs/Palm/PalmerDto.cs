using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Enums;

namespace TaroTime.Application.DTOs.Palm
{
    public record PalmerDto(
       long Id,
       string Question,
       string HandImagePath,
       string Result,
       string UserId,
       string UserName,
       PalmStatus Status,
       DateTime CreatedAt,
       string? Answer
   );

}
