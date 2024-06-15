using Booking.Application.Abstractions.Persistence.Repositories.Read;
using Booking.Application.Abstractions.Persistence.Repositories.Write;
using Booking.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Booking.Application.Abstractions.Persistence;

namespace Booking.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddDbContext<DbContext, ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("BookingConnection"));
            })
                .AddScoped<IContextTransactionCreator, ContextTransactionCreator>()
                .AddTransient(typeof(IBaseReadRepository<>), typeof(BaseRepository<>))
                .AddTransient(typeof(IBaseWriteRepository<>), typeof(BaseRepository<>))
                .AddScoped<IDatabaseMigrator, DatabaseMigrator>();
        }
    }
}