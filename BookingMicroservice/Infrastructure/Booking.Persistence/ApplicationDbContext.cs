using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Booking.Domain;

namespace Booking.Persistence
{
    public sealed class ApplicationDbContext : DbContext
    {
        #region ApplicationUsers

        internal DbSet<ApplicationUser> ApplicationUsers { get; } = default!;

        #endregion

        #region Reservations

        internal DbSet<Reservation> Reservations { get; } = default!;

        #endregion

        #region Payments

        internal DbSet<Payment> Payments { get; } = default!;
        //internal DbSet<PaymentStatus> PaymentStatuses { get; } = default!;

        #endregion

        #region Rooms

        //internal DbSet<Amenity> Amenities { get; } = default!;
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