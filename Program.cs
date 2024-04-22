using Microsoft.EntityFrameworkCore;
using Hangfire;
using AutoMail.Repository;
using AutoMail.Infrastructure;
using AutoMail.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using AutoMail.Middleware;
using AutoMail.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// ���÷���ע������ע�����
ServiceRegistration.RegisterServices(builder.Services);

// ����ʹ�����ڴ����ݿ⣬����Ը�����Ҫ���������ַ���
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseInMemoryDatabase("items"));

// ���� Identity ����
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // ��������ѡ��
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;

    // ��������...
})
.AddEntityFrameworkStores<ApplicationContext>()
.AddDefaultTokenProviders();

// ��� Hangfire ����
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

builder.Services.AddHangfireServer();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// ����Identity
app.UseAuthentication();
app.UseAuthorization();

app.Run();