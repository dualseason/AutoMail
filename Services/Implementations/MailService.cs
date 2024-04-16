using AutoMail.Services.Interfaces;

namespace AutoMail.Services.Implementations
{
    public class MailService : IMailService
    {
        public void SendMail()
        {
            Console.WriteLine("发送邮箱方法调用成功");
        }
    }
}
