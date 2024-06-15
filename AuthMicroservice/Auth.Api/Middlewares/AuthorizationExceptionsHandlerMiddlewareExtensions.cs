using System.Net;
using Auth.Application.Exceptions;
using Newtonsoft.Json;

namespace Auth.Api.Middlewares
{
    internal class AuthorizationExceptionsHandlerMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (UnauthorizedException exception)
            {
                await HandleExceptionAsync(context, exception);
            }
            catch (ForbiddenException exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch (exception)
            {
                case UnauthorizedException unauthorized:
                    code = HttpStatusCode.Unauthorized;
                    //result = JsonSerializer.Serialize(unauthorized.Message);
                    result = JsonConvert.SerializeObject(unauthorized.Message);
                    break;
                case ForbiddenException forbidden:
                    code = HttpStatusCode.Forbidden;
                    //result = JsonSerializer.Serialize(forbidden.Message);
                    result = JsonConvert.SerializeObject(forbidden.Message);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }

    /// <summary>
    /// Class for authorization error extension method
    /// </summary>
    public static class AuthorizationExceptionsHandlerMiddlewareExtensions
    {
        /// <summary>
        /// Authorization error extension method
        /// </summary>
        public static IApplicationBuilder UseAuthExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationExceptionsHandlerMiddleware>();
        }
    }
}