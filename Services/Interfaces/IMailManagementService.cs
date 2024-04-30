using AutoMail.Models.Entities;

namespace AutoMail.Services.Interfaces
{
    public interface IMailManagementService
    {
        Task SendMailAsync(int EmailConfigID, string receiveEmail, string subject, string body);
        Task<EmailConfiguration> AddEmailConfigurationAsync(EmailConfiguration emailConfiguration);
        Task<List<EmailConfiguration>> GetAllEmailConfigurationsAsync();
        Task<EmailConfiguration> GetEmailConfigurationByIdAsync(int id);
        Task UpdateEmailConfigurationAsync(int id, EmailConfiguration updatedEmailConfiguration);
        Task DeleteEmailConfigurationAsync(int id);
    }
}
