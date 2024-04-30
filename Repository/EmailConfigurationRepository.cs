using AutoMail.Models.Entities;
using SqlSugar;

namespace AutoMail.Repository
{
    public class EmailConfigurationRepository(ISqlSugarClient dbContext) : IEmailConfigurationRepository
    {
        private readonly ISqlSugarClient _dbContext = dbContext;

        public EmailConfiguration GetByEmailConfigurationId(int id)
        {
            return _dbContext.Queryable<EmailConfiguration>().Where(x => x.ID == id).First();
        }

        public List<EmailConfiguration> GetAllEmailConfigurations()
        {
            return _dbContext.Queryable<EmailConfiguration>().Includes(t => t.User).ToList();
        }

        public void AddEmailConfiguration(EmailConfiguration emailConfiguration)
        {
            _dbContext.Insertable(emailConfiguration).ExecuteCommand();
        }

        public void UpdateEmailConfiguration(EmailConfiguration emailConfiguration)
        {
            _dbContext.Updateable(emailConfiguration).ExecuteCommand();
        }

        public void DeleteEmailConfiguration(int id)
        {
            _dbContext.Deleteable<EmailConfiguration>().Where(x => x.ID == id).ExecuteCommand();
        }
    }
}
