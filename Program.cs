using Hangfire;
using AutoMail.Infrastructure;
using AutoMail.Services.Implementations;
using AutoMail.Middleware;
using Microsoft.OpenApi.Models;
using SqlSugar;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// ���÷���ע������ע�����
ServiceRegistration.RegisterServices(builder.Services);

//ע�������ģ�AOP������Ի�ȡIOC����
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
                //֧��string?��string  
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

// ��� Hangfire ����
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("SQLConnection")));

builder.Services.AddHangfireServer();

// ����Controller��
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

app.UseAuthentication();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();