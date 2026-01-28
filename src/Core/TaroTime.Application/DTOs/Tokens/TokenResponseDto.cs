using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaroTime.Application.DTOs.Tokens
{
    public record TokenResponseDto(
        string Token,
        string Username,
        DateTime Expires
        );
   
}
