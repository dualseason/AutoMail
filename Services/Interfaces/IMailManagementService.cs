using AutoMail.Models.Entitys;

namespace AutoMail.Services.Interfaces
{
    public interface IMailManagementService
    {
        Task SendMailAsync();
        Task<EmailConfiguration> AddEmailConfigurationAsync(EmailConfiguration emailConfiguration);
        Task<List<EmailConfiguration>> GetAllEmailConfigurationsAsync();
        Task<EmailConfiguration> GetEmailConfigurationByIdAsync(int id);
        Task UpdateEmailConfigurationAsync(int id, EmailConfiguration updatedEmailConfiguration);
        Task DeleteEmailConfigurationAsync(int id);
    }
}
