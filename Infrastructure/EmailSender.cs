using System.Net.Mail;
using System.Net;

namespace AutoMail.Utility
{
    public class EmailSender
    {
        private readonly string _senderEmail;
        private readonly string _senderPassword;
        private readonly string _smtpServer;
        private readonly int _smtpPort;

        public EmailSender(string senderEmail, string senderPassword, string smtpServer, int smtpPort)
        {
            _senderEmail = senderEmail;
            _senderPassword = senderPassword;
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
        }

        public async Task SendEmailAsync(string receiverEmail, string subject, string body)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(_senderEmail);
                    mail.To.Add(receiverEmail);
                    mail.Subject = subject;
                    mail.Body = body;

                    using (SmtpClient smtpClient = new SmtpClient(_smtpServer, _smtpPort))
                    {
                        smtpClient.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
                        smtpClient.EnableSsl = true;

                        await smtpClient.SendMailAsync(mail);
                    }

                    Console.WriteLine("邮件发送成功！");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发送邮件时发生错误：{ex.Message}");
                throw; // 将异常抛出以便调用者可以处理
            }
        }
    }
}
