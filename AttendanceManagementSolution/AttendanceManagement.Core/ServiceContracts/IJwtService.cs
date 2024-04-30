using AttendanceManagement.Core.Identity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AttendanceManagement.Core.ServiceContracts
{
    public interface IJwtService
    {
        JwtSecurityToken CreateJwtToken(IEnumerable<Claim> claims);
    }
}
