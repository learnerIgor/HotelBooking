using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mail.Persistence.Repositories;
using Mail.Application.Abstractions.Persistence.Repositories.Read;
using Mail.Application.Abstractions.Persistence.Repositories.Write;
using Mail.Application.Abstractions.Persistence;

namespace Mail.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddDbContext<DbContext, ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("MailConnection"));
            })
                .AddScoped<IContextTransactionCreator, ContextTransactionCreator>()
                .AddTransient(typeof(IBaseReadRepository<>), typeof(BaseRepository<>))
                .AddTransient(typeof(IBaseWriteRepository<>), typeof(BaseRepository<>))
                .AddScoped<IDatabaseMigrator, DatabaseMigrator>();
        }
    }
}