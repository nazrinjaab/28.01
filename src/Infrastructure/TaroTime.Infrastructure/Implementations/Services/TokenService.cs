using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaroTime.Application.DTOs.Tokens;
using TaroTime.Application.Interfaces.Services;
using TaroTime.Domain.Entities;

namespace TaroTime.Infrastructure.Implementations.Services
{
    internal class TokenService:ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenResponseDto CreateAccessToken(AppUser user, IEnumerable<string>roles ,int minutes)
        {
            ICollection<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),

                new Claim(ClaimTypes.Name,user.UserName),

                new Claim(ClaimTypes.Email, user.Email),

                new Claim(ClaimTypes.Surname, user.Surname),

                new Claim(ClaimTypes.GivenName,user.Name)
            };

            foreach(string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:issuer"],
                audience: _configuration["JWT:audience"],
                expires: DateTime.UtcNow.AddMinutes(minutes),
                notBefore: DateTime.UtcNow,
                claims: claims,
            
                signingCredentials: new SigningCredentials
                (new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:secretKey"])), 
                SecurityAlgorithms.HmacSha256)
                );

               return new TokenResponseDto(new JwtSecurityTokenHandler()
                .WriteToken(securityToken), 
                user.UserName,
                securityToken.ValidTo) ;
        }
    }
}
