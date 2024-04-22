using AutoMail.Models.Entities;
using AutoMail.Services.Interfaces;

namespace AutoMail.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task ChangePasswordAsync(string userId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> LoginUserAsync(string email, string password)
        {
            throw new NotImplementedException();
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
    }
}
