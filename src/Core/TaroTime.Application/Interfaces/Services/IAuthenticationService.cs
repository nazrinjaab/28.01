using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.AppUsers;
using TaroTime.Application.DTOs.Tokens;

namespace TaroTime.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        
        Task RegisterAsync(RegisterDto userDto);
        Task<TokenResponseDto> LoginAsync(LoginDto userDto);
        Task<string> ForgotPasswordAsync(ForgotPasswordDto userdto);
        Task ResetPasswordAsync(ResetPasswordDto userdto);
    }
}
