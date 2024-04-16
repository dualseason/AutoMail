using AutoMail.Services.Interfaces;
using AutoMail.Utility;

namespace AutoMail.Services.Implementations
{
    public class MailService : IMailService
    {
        public void SendMail()
        {
            // 创建 EmailSender 实例，提供发送邮件所需的参数
            EmailSender emailSender = new EmailSender("dualseason@qq.com", "pkfkqwhmigjibdah", "smtp.qq.com", 587);

            // 发送邮件
            emailSender.SendEmail("2645366211@qq.com", "邮件主题", "邮件内容");
        }
    }
}
