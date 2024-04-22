using AutoMail.Models.Entities;

namespace AutoMail.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ApplicationUser> RegisterUserAsync(string userName, string email, string password);
        Task<ApplicationUser> LoginUserAsync(string email, string password);
        Task LogoutUserAsync(string userId);
        Task ChangePasswordAsync(string userId, string newPassword);
        Task ResetPasswordAsync(string email);
    }
}
