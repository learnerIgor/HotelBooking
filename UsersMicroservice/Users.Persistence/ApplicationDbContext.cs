using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Users.Domain;

namespace Users.Persistence
{
    public sealed class ApplicationDbContext : DbContext
    {
        #region Users

        internal DbSet<ApplicationUser> ApplicationUsers { get; } = default!;
        internal DbSet<ApplicationUserRole> ApplicationUserRoles { get; } = default!;
        internal DbSet<ApplicationUserApplicationUserRole> ApplicationUserApplicationUserRole { get; } = default!;

        #endregion

        //#region Auth

        //internal DbSet<RefreshToken> RefreshTokens { get; } = default!;

        //#endregion

        //#region Todos

        //internal DbSet<Todo> Todos { get; } = default!;

        //#endregion

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