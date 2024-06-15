using System.Reflection;
using Microsoft.EntityFrameworkCore;
using HR.Domain;

namespace HR.Persistence
{
    public sealed class ApplicationDbContext : DbContext
    {
        #region Rooms

        internal DbSet<Amenity> Amenities { get; } = default!;
        internal DbSet<Room> Rooms { get; } = default!;
        internal DbSet<RoomType> RoomTypes { get; } = default!;

        #endregion

        #region Hotels

        internal DbSet<Hotel> Hotels { get; } = default!;
        internal DbSet<City> Cities { get; } = default!;
        internal DbSet<Country> Countries { get; } = default!;
        internal DbSet<Address> Address { get; } = default!;

        #endregion

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