using Microsoft.Extensions.DependencyInjection;
using Booking.Application.Abstractions;

namespace Booking.Exchanger
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddExchangeProviders(this IServiceCollection services)
        {
            return services.AddTransient<IMqEmailService, MqEmailService>();
        }
    }
}
