using AutoMail.Services.Implementations;
using AutoMail.Services.Interfaces;

namespace AutoMail.Services.impl
{
    public class MailBackgroundTask : IBackgroundTask
    {
        private readonly IServiceProvider _serviceProvider;

        public MailBackgroundTask(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ScheduleBackgroundExecute()
        {
            var mailService = (IMailService)_serviceProvider.GetRequiredService(typeof(MailService));
            mailService.SendMail();
        }
    }
}
