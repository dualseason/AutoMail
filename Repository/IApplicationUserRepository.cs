using AutoMail.Models.Entities;

namespace AutoMail.Repository
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser?> GetAllUsers();
        ApplicationUser? GetUserById(int id);
        ApplicationUser? GetUserByName(string name);
        void AddUser(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
        void DeleteUser(int id);
    }
}
