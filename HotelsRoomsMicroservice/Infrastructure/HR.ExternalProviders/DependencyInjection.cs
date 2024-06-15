using HR.Application.Abstractions.ExternalProviders;
using Microsoft.Extensions.DependencyInjection;

namespace HR.ExternalProviders;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalProviders(this IServiceCollection services)
    {
        return services
            .AddTransient<IHotelProvider, HotelProvider>()
            .AddTransient<IRoomProvider, RoomProvider>()
            .AddTransient<IRoomTypeProvider, RoomTypeProvider>()
            .AddTransient<ICountryProvider, CountryProvider>()
            .AddTransient<ICityProvider, CityProvider>();
    }
}