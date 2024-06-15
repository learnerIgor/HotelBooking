using FluentValidation;
using HR.Application.Abstractions.ExternalProviders;
using HR.Application.Behavior;
using HR.Application.Caches;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HR.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHotelRoomApplicationServices(this IServiceCollection services)
        {
            return services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
            .AddTransient<ICleanHotelRoomCacheService, CleanHotelRoomCacheService>()
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(DatabaseTransactionBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizePermissionsBehavior<,>));
        }
    }
}
