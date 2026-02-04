using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Enums;

namespace TaroTime.Application.DTOs.Coffees
{
    public record CoffeerDto(
       long Id,
       string Question,
       string CupImagePath,
     
       string UserId,
       //string UserName,
       CoffeeStatus Status,
       DateTime CreatedAt,
       string? Answer
   );

}
