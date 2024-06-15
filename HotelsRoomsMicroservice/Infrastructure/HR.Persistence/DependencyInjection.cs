using HR.Application.Abstractions.Persistence;
using HR.Application.Abstractions.Persistence.Repositories.Read;
using HR.Application.Abstractions.Persistence.Repositories.Write;
using HR.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddDbContext<DbContext, ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("HotelRoomConnection"));
            })
                .AddScoped<IContextTransactionCreator, ContextTransactionCreator>()
                .AddTransient(typeof(IBaseReadRepository<>), typeof(BaseRepository<>))
                .AddTransient(typeof(IBaseWriteRepository<>), typeof(BaseRepository<>))
                .AddScoped<IDatabaseMigrator, DatabaseMigrator>();
        }
    }
}