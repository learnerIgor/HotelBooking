using Auth.Application.Abstractions.ExternalProviders;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.ExternalProviders;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalProviders(this IServiceCollection services)
    {
        return services
            .AddTransient<IUsersProvider, UsersGrpcProvider>();
    }
}