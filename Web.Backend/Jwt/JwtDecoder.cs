using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Web.Backend.BLL.UtilityMethods;

namespace Web.Backend.Jwt
{
    public static class JwtDecoder
    {
        public static DateTime ReadExpiredate(string jwtToken)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var decodedToken = jwtHandler.ReadJwtToken(jwtToken);
            var expirationTime = decodedToken.Payload.Exp;

            var expireDateTime = DateTimeUtility.ConvertUnixToDateTime((long) expirationTime.Value);

            return expireDateTime;
        }
        public static int ReadUserId(string jwtToken)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var decodedToken = jwtHandler.ReadJwtToken(jwtToken);
            var claims = decodedToken.Claims;

            var Id = claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            var response = Convert.ToInt32(Id);

            return response;
        }

        public static string ReadUserRole(string jwtToken)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var decodedToken = jwtHandler.ReadJwtToken(jwtToken);
            var claims = decodedToken.Claims;

            var role = claims.FirstOrDefault(c => c.Type == "Role")?.Value;

            return role;
        }


    }
}
