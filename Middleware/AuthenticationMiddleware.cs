using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AutoMail.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public AuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            if (IsAuthenticationRequired(context))
            {
                string? token = context.Request.Headers["Authorization"];
                if (token != null)
                {
                    await ValidateToken(context, token);
                }
                else
                {
                    // Unauthorized
                    context.Response.StatusCode = 401; 
                    return;
                }
            }
            await _next(context);
        }
        private Task ValidateToken(HttpContext context, string token)
        {
            try
            {
                string? secretKey = _configuration["Jwt:SecretKey"];
                if (!string.IsNullOrEmpty(secretKey))
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                    var tokenHandler = new JwtSecurityTokenHandler();

                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _configuration["Jwt:Issuer"],
                        ValidAudience = _configuration["Jwt:Audience"],
                        IssuerSigningKey = key
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    context.Items["jwtPayload"] = jwtToken.Payload; // You can access token claims via context.Items
                    return Task.CompletedTask;
                }
                else
                {
                    throw new Exception("获取Jwt:SecretKey失败");
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 401; // Unauthorized
                return Task.FromException(ex);
            }
        }

        private bool IsAuthenticationRequired(HttpContext context)
        {
            return context.GetEndpoint()?.Metadata.GetMetadata<RequireAuthenticationAttribute>() != null;
        }
    }

    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class RequireAuthenticationAttribute : Attribute
    {

    }
}
