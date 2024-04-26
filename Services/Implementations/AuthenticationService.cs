using AutoMail.Infrastructure;
using AutoMail.Models.Entities;
using AutoMail.Models.ViewModels;
using AutoMail.Repository;
using AutoMail.Services.Interfaces;

namespace AutoMail.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IApplicationUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IApplicationUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public Task ChangePasswordAsync(string userId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<LoginUserResult> LoginUserAsync(string email, string password)
        {
            // 在这里进行用户身份验证逻辑
            if (IsValidUser(email, password))
            {
                // 如果用户身份验证成功，生成 JWT 令牌
                var token = new AuthenticationGenerator(_configuration).GenerateUserToken(email);
                ApplicationUser? applicationUser = _userRepository.GetUserByName(email);
                var result = new LoginUserResult
                {
                    Success = true,
                    Message = "登录成功",
                    Token = token,
                    ApplicationUser = applicationUser
                };
                return Task.FromResult(result);
            }
            else
            {
                // 如果用户身份验证失败，返回空字符串或者抛出异常，视情况而定
                throw new Exception("用户身份验证失败");
            }
        }

        public Task LogoutUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> RegisterUserAsync(string userName, string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task ResetPasswordAsync(string email)
        {
            throw new NotImplementedException();
        }

        private bool IsValidUser(string username, string password)
        {
            // 在这里编写用户身份验证逻辑，例如验证用户名和密码是否匹配数据库中的记录
            // 这里只是一个简单的示例，实际情况下应该根据你的应用程序的实际需求来进行验证逻辑
            return username == "user" && password == "password";
        }
    }
}
