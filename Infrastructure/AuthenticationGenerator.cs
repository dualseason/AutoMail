using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace AutoMail.Infrastructure
{
    public class AuthenticationGenerator
    {
        public readonly IConfiguration _configuration;

        public AuthenticationGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string GenerateToken(string secretKey, string issuer, string audience, DateTime expiration, string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 添加用户账户名到声明
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return tokenHandler.WriteToken(token);
        }

        public string GenerateUserToken(string username)
        {
            // 设置JWT参数
            string? secretKey = _configuration["Jwt:SecretKey"]; 
            string? issuer = _configuration["Jwt:Issuer"];
            string? audience = _configuration["Jwt:Audience"];
            // 令牌过期时间，例如：1小时后
            DateTime expiration = DateTime.UtcNow.AddHours(1); 

            if (!string.IsNullOrEmpty(secretKey) && !string.IsNullOrEmpty(issuer) && !string.IsNullOrEmpty(audience))
            {
                // 调用 GenerateToken 方法生成 JWT 令牌
                string jwtToken = AuthenticationGenerator.GenerateToken(secretKey, issuer, audience, expiration, username);

                // 返回生成的 JWT 令牌
                return jwtToken;
            }
            return string.Empty;
        }
    }
}
