using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaroTime.Application.DTOs.Palm
{
    public record PalmReadingDto(
        long Id,
        string Result, 
        string UserId
        );
    
}
