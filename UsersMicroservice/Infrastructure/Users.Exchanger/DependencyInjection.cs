using Microsoft.Extensions.DependencyInjection;
using Users.Application.Abstractions;

namespace Users.Exchanger
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddExchangeProviders(this IServiceCollection services)
        {
            return services.AddTransient<IMqService, MqService>();
        }
    }
}
