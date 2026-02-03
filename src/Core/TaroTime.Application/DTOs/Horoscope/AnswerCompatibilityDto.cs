using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaroTime.Application.DTOs.Horoscope
{
    public record AnswerCompatibilityDto(
    long CompatibilityId,
    int CompatibilityPercent,
    string Description
);
}
