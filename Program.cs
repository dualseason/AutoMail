using Microsoft.EntityFrameworkCore;
using Hangfire;
using AutoMail.Repository;
using AutoMail.Infrastructure;
using AutoMail.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// 调用服务注册类来注册服务
ServiceRegistration.RegisterServices(builder.Services);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseInMemoryDatabase("items")); // 这里使用了内存数据库，你可以根据需要更改连接字符串

// 添加 Hangfire 服务。
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

// 测试环境启动Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard();

// 获取后台任务管理器实例并调度后台任务
var backgroundTaskManager = new BackgroundTaskManager(app.Services);
backgroundTaskManager.ScheduleBackgroundTasks();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();