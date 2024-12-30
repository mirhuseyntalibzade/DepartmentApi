using DepartmentBL.ExternalServices.Interfaces;
using DepartmentCore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentBL.ExternalServices.Implementations
{
    public class TokenService : ITokenService
    {
        readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(AppUser appUser)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("FirstName", appUser.FirstName),
                new Claim("LastName", appUser.LastName),
                new Claim("UserName", appUser.UserName),
                new Claim(ClaimTypes.NameIdentifier,appUser.Id)
            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JwtSettings:ExpiryInMinutes"]))
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
