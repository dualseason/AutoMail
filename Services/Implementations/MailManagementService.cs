using AutoMail.Models.Entitys;
using AutoMail.Repository;
using AutoMail.Services.Interfaces;
using AutoMail.Utility;

namespace AutoMail.Services.Implementations
{
    public class MailManagementService : IMailManagementService
    {
        private readonly IEmailConfigurationRepository _emailConfigurationRepository;

        public MailManagementService(IEmailConfigurationRepository emailConfigurationRepository)
        {
            _emailConfigurationRepository = emailConfigurationRepository;
        }


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

        public async Task SendMailAsync(int EmailConfigID, string receiveEmail, string subject, string body)
        {
            // 根据 EmailConfigID 获取邮件配置信息
            EmailConfiguration emailConfiguration = _emailConfigurationRepository.GetByEmailConfigurationId(EmailConfigID);

            // 如果找到了邮件配置信息
            if (emailConfiguration != null)
            {
                // 创建 EmailSender 实例，提供发送邮件所需的参数
                var emailSender = new EmailSender(emailConfiguration.UserName, emailConfiguration.Password, emailConfiguration.SmtpServer, emailConfiguration.SmtpPort);

                try
                {
                    // 发送邮件
                    await emailSender.SendEmailAsync(receiveEmail, subject, body);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
            {
                throw new ArgumentException("Invalid EmailConfigID");
            }
        }

        public Task UpdateEmailConfigurationAsync(int id, EmailConfiguration updatedEmailConfiguration)
        {
            throw new NotImplementedException();
        }
    }
}
