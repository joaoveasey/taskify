﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Taskify.API.Interfaces
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration configuration);
    }
}
