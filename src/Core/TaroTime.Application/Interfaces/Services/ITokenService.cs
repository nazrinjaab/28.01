using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Tokens;
using TaroTime.Domain.Entities;

namespace TaroTime.Application.Interfaces.Services
{
    public interface ITokenService
    {
        TokenResponseDto CreateAccessToken(AppUser user, IEnumerable<string> roles, int minutes);
    }
}
