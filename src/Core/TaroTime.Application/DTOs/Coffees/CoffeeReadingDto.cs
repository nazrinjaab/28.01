using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Enums;

namespace TaroTime.Application.DTOs.Coffees
{
    public record CoffeeReadingDto(
       long Id,
       string Question,
       string? Answer,
       string UserId,
       //string UserName,
       string CoffeeReaderId,
       CoffeeStatus Status
        );


}
