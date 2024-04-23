using Microsoft.EntityFrameworkCore;
using Hangfire;
using AutoMail.Repository;
using AutoMail.Infrastructure;
using AutoMail.Services.Implementations;
using AutoMail.Middleware;


var builder = WebApplication.CreateBuilder(args);

// ���÷���ע������ע�����
ServiceRegistration.RegisterServices(builder.Services);

// ����ʹ�����ڴ����ݿ⣬����Ը�����Ҫ���������ַ���
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HangfireConnection"))
);

// ����IdentityServer
builder.Services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(IdentityConfiguration.Clients)
                .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
                .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
                .AddTestUsers(IdentityConfiguration.Users);

// ��� Hangfire ����
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

builder.Services.AddHangfireServer();

// ����Controller��
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ���������֤
app.UseAuthenticationMiddleware();

// ���Ի�������Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard();

// ��ȡ��̨���������ʵ�������Ⱥ�̨����
var backgroundTaskManager = new BackgroundTaskManager(app.Services);
backgroundTaskManager.ScheduleBackgroundTasks();

// ����IdentityServer
app.UseIdentityServer();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();