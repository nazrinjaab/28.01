using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Entities;
using TaroTime.Domain.Enums;

namespace TaroTime.Application.DTOs.Horoscope
{
    public record CreateCompatibilityDto(
    //long Id,
    ZodiacSign UserZodiac,
    ZodiacSign PartnerZodiac,
    DateTime UserBirthDate,       
    DateTime PartnerBirthDate    
    );

}
