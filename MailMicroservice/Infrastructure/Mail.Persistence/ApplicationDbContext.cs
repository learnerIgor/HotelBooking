using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Mail.Domain;

namespace Mail.Persistence
{
    public sealed class ApplicationDbContext : DbContext
    {
        internal DbSet<EmailHistory> EmailHistories { get; } = default!;


        #region Ef

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}