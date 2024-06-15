using Booking.Application.Handlers.Booking.Queries.GetBookingsCount;
using Booking.Domain;
using System.Linq.Expressions;

namespace Booking.Application.Handlers.Booking.Queries
{
    internal static class ListBookingWhere
    {
        public static Expression<Func<Reservation, bool>> Where(GetBookingsCountQuery getBookings)
        {
            var startDate = DateTime.Parse(getBookings.StartDate);
            var endDate = DateTime.Parse(getBookings.EndDate);
            return bookings => (bookings.CheckInDate > startDate && bookings.CheckOutDate < endDate) && bookings.IsActive;
        }
    }
}