using Auth.Application.Behavior;
using Auth.Application.Caches;
using Auth.Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Auth.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthApplicationServices(this IServiceCollection services)
        {
            return services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddTransient<ICreateJwtTokenService, CreateJwtTokenService>()
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
                //.AddSingleton<ApplicationUsersListMemoryCache>()
                .AddSingleton<ApplicationUserMemoryCache>()
                //.AddSingleton<ApplicationUsersCountMemoryCache>()
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(DatabaseTransactionBehavior<,>));
        }
    }
}
