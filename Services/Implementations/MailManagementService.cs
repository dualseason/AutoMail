using AutoMail.Models.Entities;
using AutoMail.Services.Interfaces;
using SqlSugar;

namespace AutoMail.Services.Implementations
{
    public class MailManagementService(ISqlSugarClient dbContext, ILogger<AuthenticationService> logger) : IMailManagementService
    {
        private readonly ISqlSugarClient _dbContext = dbContext;
        private readonly ILogger<AuthenticationService> _logger = logger;

        public Task<EmailConfiguration> AddEmailConfigurationAsync(EmailConfiguration emailConfiguration)
        {
            try
            {
                _dbContext.Insertable(emailConfiguration).ExecuteCommand();
                return Task.FromResult(emailConfiguration);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw;
            }
        }

        public Task DeleteEmailConfigurationAsync(int id)
        {
            _dbContext.Deleteable<EmailConfiguration>().Where(x => x.ID == id).ExecuteCommand();
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
            //// 根据 EmailConfigID 获取邮件配置信息
            //EmailConfiguration? emailConfiguration = _emailConfigurationRepository.GetByEmailConfigurationId(EmailConfigID);

            //// 如果找到了邮件配置信息
            //if (emailConfiguration != null)
            //{
            //    // 创建 EmailSender 实例，提供发送邮件所需的参数
            //    var emailSender = new EmailSender(emailConfiguration.UserName, emailConfiguration.Password, emailConfiguration.SmtpServer, emailConfiguration.SmtpPort);

            //    try
            //    {
            //        // 发送邮件
            //        await emailSender.SendEmailAsync(receiveEmail, subject, body);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.ToString());
            //    }
            //}
            //else
            //{
            //    throw new ArgumentException("Invalid EmailConfigID");
            //}
        }

        public Task UpdateEmailConfigurationAsync(int id, EmailConfiguration updatedEmailConfiguration)
        {
            throw new NotImplementedException();
        }
    }
}
