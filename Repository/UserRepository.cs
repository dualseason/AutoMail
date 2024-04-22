using AutoMail.Models.Entities;

namespace AutoMail.Repository
{
    public class UserRepository(ApplicationContext dbContext) : IUserRepository
    {
        private readonly ApplicationContext _dbContext = dbContext;

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public ApplicationUser? GetUserById(int id)
        {
            return _dbContext.Users.Find(id);
        }

        public void AddUser(ApplicationUser user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void UpdateUser(ApplicationUser user)
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = _dbContext.Users.Find(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }
    }
}
