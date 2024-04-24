using Microsoft.EntityFrameworkCore;
using Hangfire;
using AutoMail.Repository;
using AutoMail.Infrastructure;
using AutoMail.Services.Implementations;
using AutoMail.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// 调用服务注册类来注册服务
ServiceRegistration.RegisterServices(builder.Services);

// 这里使用了内存数据库，你可以根据需要更改连接字符串
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HangfireConnection"))
);

// 配置IdentityServer
builder.Services.AddIdentityServer()
     //添加证书加密方式，执行该方法，会先判断tempkey.rsa证书文件是否存在，如果不存在的话，就创建一个新的tempkey.rsa证书文件，如果存在的话，就使用此证书文件。
     .AddDeveloperSigningCredential()
     .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
     //把受保护的Api资源添加到内存中
     .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
     //客户端配置添加到内存中
     .AddInMemoryClients(IdentityConfiguration.Clients)
     //测试的用户添加进来
     .AddTestUsers(IdentityConfiguration.Users);

//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", option =>
//    {
//        //oidc的服务地址(一定要用 Https)
//        //也就是IdentifyServer项目运行地址
//        option.Authority = "https://localhost:7133";
//        option.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateAudience = false
//        };
//    });

////注册授权服务
//builder.Services.AddAuthorization(option =>
//{
//    //添加授权策略
//    option.AddPolicy("MyApiScope", opt =>
//    {
//        //配置鉴定用户的规则，也就是说必须通过身份认证
//        opt.RequireAuthenticatedUser();
//        //鉴定api范围的规则
//        opt.RequireClaim("scope", "simple_api");
//    });
//});

// 添加 Hangfire 服务。
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

builder.Services.AddHangfireServer();

// 开启Controller层
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// 开启IdentityServer
app.UseIdentityServer();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();