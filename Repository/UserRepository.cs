using AutoMail.Models.Entities;

namespace AutoMail.Repository
{
    public class UserRepository(DataContext dbContext) : IUserRepository
    {
        private readonly DataContext _dbContext = dbContext;

        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public User? GetUserById(int id)
        {
            return _dbContext.Users.Find(id);
        }

        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void UpdateUser(User user)
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
