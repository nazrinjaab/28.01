using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaroTime.Application.DTOs.AppUsers
{
    public record ResetPasswordDto(
        string UsernameOrEmail,
        string Token,
        string NewPassword,
        string ConfirmPassword
        );
    
}
