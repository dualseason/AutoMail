using Microsoft.EntityFrameworkCore;
using Hangfire;
using AutoMail.Repository;
using AutoMail.Infrastructure;
using AutoMail.Services.Implementations;
using AutoMail.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// ���÷���ע������ע�����
ServiceRegistration.RegisterServices(builder.Services);

// ����ʹ�����ڴ����ݿ⣬����Ը�����Ҫ���������ַ���
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HangfireConnection"))
);

// ����IdentityServer
builder.Services.AddIdentityServer()
     //���֤����ܷ�ʽ��ִ�и÷����������ж�tempkey.rsa֤���ļ��Ƿ���ڣ���������ڵĻ����ʹ���һ���µ�tempkey.rsa֤���ļ���������ڵĻ�����ʹ�ô�֤���ļ���
     .AddDeveloperSigningCredential()
     .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
     //���ܱ�����Api��Դ��ӵ��ڴ���
     .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
     //�ͻ���������ӵ��ڴ���
     .AddInMemoryClients(IdentityConfiguration.Clients)
     //���Ե��û���ӽ���
     .AddTestUsers(IdentityConfiguration.Users);

//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", option =>
//    {
//        //oidc�ķ����ַ(һ��Ҫ�� Https)
//        //Ҳ����IdentifyServer��Ŀ���е�ַ
//        option.Authority = "https://localhost:7133";
//        option.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateAudience = false
//        };
//    });

////ע����Ȩ����
//builder.Services.AddAuthorization(option =>
//{
//    //�����Ȩ����
//    option.AddPolicy("MyApiScope", opt =>
//    {
//        //���ü����û��Ĺ���Ҳ����˵����ͨ�������֤
//        opt.RequireAuthenticatedUser();
//        //����api��Χ�Ĺ���
//        opt.RequireClaim("scope", "simple_api");
//    });
//});

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

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();