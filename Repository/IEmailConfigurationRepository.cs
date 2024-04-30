using AutoMail.Models.Entities;

namespace AutoMail.Repository
{
    public interface IEmailConfigurationRepository
    {
        EmailConfiguration? GetByEmailConfigurationId(int id);
        List<EmailConfiguration> GetAllEmailConfigurations();
        void AddEmailConfiguration(EmailConfiguration emailConfiguration);
        void UpdateEmailConfiguration(EmailConfiguration emailConfiguration);
        void DeleteEmailConfiguration(int id);
    }
}
