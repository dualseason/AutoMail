using AutoMail.Services.impl;
using AutoMail.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutoMail.Services.Implementations
{
    public class ServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // 使用 AddTransient 添加服务
            services.AddTransient<MailBackgroundTask>();
            services.AddTransient<IMailService, MailService>();
            // 注册更多的服务...
        }
    }
}
