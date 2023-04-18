using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Web.Backend.Jwt
{
    public class JwtTokenGenerator
    {
        public string GenerateToken(int userId, string username, string role, DateTime expireDateTime, JwtConfig config)
        {
            var _secretKey = config.Key;
            var _issuer = config.Issuer;
            var _audience = config.Audience;
            var claims = new[]
            {
            new Claim("Id", userId.ToString()),
            new Claim("Username", username),
            new Claim("Role", role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: expireDateTime,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
