using Microsoft.EntityFrameworkCore;
using Hangfire;
using AutoMail.Repository;
using AutoMail.Infrastructure;
using AutoMail.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// ���÷���ע������ע�����
ServiceRegistration.RegisterServices(builder.Services);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseInMemoryDatabase("items")); // ����ʹ�����ڴ����ݿ⣬����Ը�����Ҫ���������ַ���

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

app.Run();