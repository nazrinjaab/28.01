using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaroTime.Application.DTOs.Accounts
{
    public record MakeRolesDto(
        string UserId,
        string RoleName
        );
    
}
