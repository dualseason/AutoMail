using AutoMail.Models.Entities;

namespace AutoMail.Models.ViewModels
{
    public class LoginUserResult: BaseResult
    {
        public string? Token { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }
    }
}
