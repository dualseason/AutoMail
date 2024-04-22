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
            //var _mailService = (IMailManagementService)_serviceProvider.GetRequiredService(typeof(IMailManagementService));
            //_mailService.SendMailAsync();
        }
    }
}
