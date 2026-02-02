using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Domain.Entities;
using TaroTime.Domain.Enums;

namespace TaroTime.Application.DTOs.Horoscope
{
    public record DailyHoroscopeDto(
    ZodiacSign Zodiac,
    ZodiacElement Element,
    DateTime Today,
    string Description
    );

}
