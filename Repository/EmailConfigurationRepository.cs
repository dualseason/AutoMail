using AutoMail.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace AutoMail.Repository
{
    public class EmailConfigurationRepository : IEmailConfigurationRepository
    {
        private readonly DataContext _dbContext;

        public EmailConfigurationRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public EmailConfiguration? GetByEmailConfigurationId(int id)
        {
            return _dbContext.EmailConfigurations.FirstOrDefault(ec => ec.ID == id);
        }

        public List<EmailConfiguration> GetAllEmailConfigurations()
        {
            return [.. _dbContext.EmailConfigurations];
        }

        public void AddEmailConfiguration(EmailConfiguration emailConfiguration)
        {
            _dbContext.EmailConfigurations.Add(emailConfiguration);
            _dbContext.SaveChanges();
        }

        public void UpdateEmailConfiguration(EmailConfiguration emailConfiguration)
        {
            _dbContext.Entry(emailConfiguration).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteEmailConfiguration(int id)
        {
            var emailConfiguration = _dbContext.EmailConfigurations.Find(id);
            if (emailConfiguration != null)
            {
                _dbContext.EmailConfigurations.Remove(emailConfiguration);
                _dbContext.SaveChanges();
            }
        }
    }
}
