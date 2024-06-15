using Booking.Application.Abstractions.ExternalProviders;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.ExternalProviders;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalProviders(this IServiceCollection services)
    {
        return services.AddTransient<IBookingProvider, BookingProvider>()
            .AddTransient<IRoomProvider, RoomsGrpcProvider>();
    }
}