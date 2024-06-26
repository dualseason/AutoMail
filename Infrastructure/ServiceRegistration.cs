﻿using AutoMail.Repository;
using AutoMail.Services.impl;
using AutoMail.Services.Interfaces;

namespace AutoMail.Services.Implementations
{
    public class ServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // 使用 AddTransient 添加服务
            services.AddTransient<MailBackgroundTask>();
            services.AddTransient<IMailManagementService, MailManagementService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            // 注册更多的服务...

            // 注册 IUserRepository 接口的实现类
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IEmailConfigurationRepository, EmailConfigurationRepository>();
        }
    }
}
