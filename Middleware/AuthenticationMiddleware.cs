using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace AutoMail.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // 判断请求的目标端点是否需要进行身份验证
            if (RequiresAuthentication(context))
            {
                if (!context.User.Identity.IsAuthenticated)
                {
                    // 如果用户未经过身份验证，返回 401 未经授权的状态码
                    context.Response.StatusCode = 401;
                    return;
                }
            }

            // 继续执行其他中间件逻辑
            await _next(context);
        }

        // 判断请求的目标端点是否需要进行身份验证
        private bool RequiresAuthentication(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint == null)
            {
                // 如果端点为空，则不需要进行身份验证
                return false;
            }

            // 根据端点的信息判断是否需要进行身份验证
            var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            if (controllerActionDescriptor == null)
            {
                // 如果端点不是控制器动作方法，则不需要进行身份验证
                return false;
            }

            // 判断控制器动作方法是否标记了 [Authorize] 特性，或者其他需要身份验证的特性
            return controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true).Any(attr => attr is AuthorizeAttribute);
        }
    }

    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }

}
