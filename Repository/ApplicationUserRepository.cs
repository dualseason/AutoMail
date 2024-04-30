using AutoMail.Models.Entities;
using SqlSugar;

namespace AutoMail.Repository
{
    public class ApplicationUserRepository(ISqlSugarClient dbContext) : IApplicationUserRepository
    {
        private readonly ISqlSugarClient _dbContext = dbContext;

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _dbContext.Queryable<ApplicationUser>().Includes(x => x.EmailConfigurations).ToList();
        }

        public ApplicationUser? GetUserById(int id)
        {
            return _dbContext.Queryable<ApplicationUser>().Where(x => x.ID == id).First();
        }

        public void AddUser(ApplicationUser user)
        {
            _dbContext.Insertable(user).ExecuteCommand();
        }

        public void UpdateUser(ApplicationUser user)
        {
            _dbContext.Updateable(user).ExecuteCommand();
        }

        public void DeleteUser(int id)
        {
            _dbContext.Deleteable<ApplicationUser>().Where(x => x.ID == id).ExecuteCommand();
        }

        public ApplicationUser GetUserByName(string name)
        {
            return _dbContext.Queryable<ApplicationUser>().Where(x => x.UserName == name).First();
        }
    }
}
