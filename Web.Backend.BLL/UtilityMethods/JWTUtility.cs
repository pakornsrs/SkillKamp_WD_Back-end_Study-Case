using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Web.Backend.BLL.UtilityMethods
{
    public static class JWTUtility
    {

        public static string GenerateToken(string userId, string username, string role, DateTime expireDateTime)
        {
            var _secretKey = "90ba3d3ad15e11edafa10242ac1200023ea707bfd8664e48b27620194512154a";
            var _issuer = "localhost";
            var claims = new[]
            {
            new Claim("Id", userId),
            new Claim("Username", username),
            new Claim("Role", role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _issuer,
                claims: claims,
                expires: expireDateTime,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}
