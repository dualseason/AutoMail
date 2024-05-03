using Hangfire;
using AutoMail.Infrastructure;
using AutoMail.Services.Implementations;
using AutoMail.Middleware;
using Microsoft.OpenApi.Models;
using SqlSugar;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// 调用服务注册类来注册服务
ServiceRegistration.RegisterServices(builder.Services);

//注册上下文：AOP里面可以获取IOC对象
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<ISqlSugarClient>(option =>
{
    SqlSugarScope sqlSugar = new SqlSugarScope(new ConnectionConfig()
    {
        DbType = DbType.SqlServer,
        ConnectionString = builder.Configuration.GetConnectionString("SQLConnection"),
        IsAutoCloseConnection = true,
        ConfigureExternalServices = new ConfigureExternalServices
        {
            EntityService = (c, p) =>
            {
                //支持string?和string  
                if (p.IsPrimarykey == false && new NullabilityInfoContext().Create(c).WriteState is NullabilityState.Nullable)
                {
                    p.IsNullable = true;
                }
            }
        }
    },
    db =>
    {
        if (!Const.InitDataBase)
        {
            db.DbMaintenance.CreateDatabase();

            db.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(Program).Assembly.GetTypes()
            .Where(type => type.FullName.Contains("AutoMail.Models.Entities"))
            .Where(type => !type.FullName.Contains("AutoMail.Models.Entities.BaseEntity"))
            .ToArray());
            Const.InitDataBase = true;
        }
       
        db.Aop.OnLogExecuting = (sql, pars) =>
        {
            if (builder.Environment.IsDevelopment())
            {
                Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));
            }
        };
    });
    return sqlSugar;
});

// 添加 Hangfire 服务。
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("SQLConnection")));

builder.Services.AddHangfireServer();

// 开启Controller层
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "AutoMail API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// 开启身份验证
app.UseAuthenticationMiddleware();

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

app.UseAuthentication();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();