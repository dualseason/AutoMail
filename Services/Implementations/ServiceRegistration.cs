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
            services.AddTransient<MailBackgroundTask>();
            services.AddTransient<MailService>();
            services.AddTransient<IMailService, MailService>(); // 使用 AddTransient 添加服务
            // 注册更多的服务...
        }
    }
}
