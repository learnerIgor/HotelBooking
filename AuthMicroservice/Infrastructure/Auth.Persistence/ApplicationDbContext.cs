using Auth.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Auth.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        #region Users
        internal DbSet<ApplicationUser> ApplicationUsers { get; } = default!;
        internal DbSet<ApplicationUserRole> ApplicationUserRoles { get; } = default!;

        #endregion

        #region Auth
        internal DbSet<RefreshToken> RefreshTokens { get; } = default!;
        #endregion

        #region EF
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
