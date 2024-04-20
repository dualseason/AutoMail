using AutoMail.Models.Entities;

namespace AutoMail.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> RegisterUserAsync(string userName, string email, string password);
        Task<User> LoginUserAsync(string email, string password);
        Task LogoutUserAsync(string userId);
        Task ChangePasswordAsync(string userId, string newPassword);
        Task ResetPasswordAsync(string email);
    }
}
