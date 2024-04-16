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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="senderEmail"></param>
        /// <param name="senderPassword"></param>
        /// <param name="smtpServer"></param>
        /// <param name="smtpPort"></param>
        public EmailSender(string senderEmail, string senderPassword, string smtpServer, int smtpPort)
        {
            _senderEmail = senderEmail;
            _senderPassword = senderPassword;
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
        }

        public void SendEmail(string receiverEmail, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(_senderEmail);
                mail.To.Add(receiverEmail);
                mail.Subject = subject;
                mail.Body = body;

                SmtpClient smtpClient = new SmtpClient(_smtpServer);
                smtpClient.Port = _smtpPort;
                smtpClient.Credentials = new NetworkCredential(_senderEmail, _senderPassword);
                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);
                Console.WriteLine("邮件发送成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发送邮件时发生错误：{ex.Message}");
            }
        }
    }
}
