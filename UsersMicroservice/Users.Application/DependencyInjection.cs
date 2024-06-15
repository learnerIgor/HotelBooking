using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Users.Application.Behavior;
using Users.Application.Caches;

namespace Users.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUsersApplicationsServices(this IServiceCollection services)
        {
            return services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
                .AddSingleton<ApplicationUsersListMemoryCache>()
                .AddSingleton<ApplicationUserMemoryCache>()
                .AddSingleton<ApplicationUsersCountMemoryCache>()
                .AddSingleton<ApplicationUserExternalMemoryCache>()
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(DatabaseTransactionBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizePermissionsBehavior<,>));
        }
    }
}
