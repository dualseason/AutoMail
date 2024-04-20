using AutoMail.Models.Entitys;
using AutoMail.Services.Interfaces;
using AutoMail.Utility;

namespace AutoMail.Services.Implementations
{
    public class MailManagementService : IMailManagementService
    {
        public Task<EmailConfiguration> AddEmailConfigurationAsync(EmailConfiguration emailConfiguration)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEmailConfigurationAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmailConfiguration>> GetAllEmailConfigurationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EmailConfiguration> GetEmailConfigurationByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SendMailAsync()
        {
            // 创建 EmailSender 实例，提供发送邮件所需的参数
            EmailSender emailSender = new EmailSender("dualseason@qq.com", "pkfkqwhmigjibdah", "smtp.qq.com", 587);

            // 发送邮件
            emailSender.SendEmail("2645366211@qq.com", "邮件主题", "邮件内容");
            return Task.CompletedTask;
        }

        public Task UpdateEmailConfigurationAsync(int id, EmailConfiguration updatedEmailConfiguration)
        {
            throw new NotImplementedException();
        }
    }
}
