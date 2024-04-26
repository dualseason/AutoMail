using AutoMail.Models.Entities;
using AutoMail.Models.ViewModels;

namespace AutoMail.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ApplicationUser> RegisterUserAsync(string userName, string email, string password);
        Task<LoginUserResult> LoginUserAsync(string email, string password);
        Task LogoutUserAsync(string userId);
        Task ChangePasswordAsync(string userId, string newPassword);
        Task ResetPasswordAsync(string email);
    }
}
