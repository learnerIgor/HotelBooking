using Auth.Application.Abstractions.Persistence;
using Auth.Application.Abstractions.Persistence.Repositories.Read;
using Auth.Application.Abstractions.Persistence.Repositories.Write;
using Auth.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<DbContext, ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AuthConnection"));
            })
                .AddScoped<IContextTransactionCreator, ContextTransactionCreator>()
                .AddTransient(typeof(IBaseReadRepository<>), typeof(BaseRepository<>))
                .AddTransient(typeof(IBaseWriteRepository<>), typeof(BaseRepository<>))
                .AddScoped<IDatabaseMigrator, DatabaseMigrator>();
        }
    }
}