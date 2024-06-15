using Accommo.Application.Abstractions.Persistence;
using Accommo.Application.Abstractions.Persistence.Repositories.Read;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accommo.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddDbContext<DbContext, ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AccommoConnection"));
            })
                .AddScoped<IContextTransactionCreator, ContextTransactionCreator>()
                .AddTransient(typeof(IBaseReadRepository<>), typeof(BaseRepository<>))
                .AddTransient(typeof(IBaseWriteRepository<>), typeof(BaseRepository<>))
                .AddScoped<IDatabaseMigrator, DatabaseMigrator>();
        }
    }
}