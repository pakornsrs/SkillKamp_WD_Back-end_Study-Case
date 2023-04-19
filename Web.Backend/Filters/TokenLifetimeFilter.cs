using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Web.Backend.BLL.UtilityMethods;
using Web.Backend.Jwt;

namespace Web.Backend.Filters
{
    public class TokenLifetimeFilter : IActionFilter
    {
        private readonly JwtConfig jwtConfig;
        public TokenLifetimeFilter(IOptions<JwtConfig> jwtConfig)
        {
            this.jwtConfig = jwtConfig.Value;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var authHeader = context.HttpContext.Request.Headers["Authorization"];
            
            if (!string.IsNullOrEmpty(authHeader))
            {
                var token = authHeader.ToString().Split(" ")[1];

                var expireDate = JwtDecoder.ReadExpiredate(token);


            }
        }
    }

    public class TokenLifetimeException : Exception
    {
        public TokenLifetimeException(string message) : base(message)
        {
        }
    }

}
