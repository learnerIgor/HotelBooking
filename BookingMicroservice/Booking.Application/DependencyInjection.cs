﻿using Booking.Application.Behavior;
using Booking.Application.Caches;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Booking.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBookingApplicationServices(this IServiceCollection services)
        {
            return services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
                .AddTransient<ICleanBookingCacheService, CleanBookingCacheService>()
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(DatabaseTransactionBehavior<,>));
                /*.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizePermissionsBehavior<,>));*/
        }
    }
}
