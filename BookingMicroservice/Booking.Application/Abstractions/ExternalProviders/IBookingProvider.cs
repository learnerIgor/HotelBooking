using Booking.Domain;

namespace Booking.Application.Abstractions.ExternalProviders
{
    public interface IBookingProvider
    {
        Task AddBookingAsync(string token, Reservation reservation, CancellationToken cancellationToken);
        Task UpdateBookingAsync(string token, string reservationId, Reservation reservation, CancellationToken cancellationToken);
        Task DeleteBookingAsync(string token, Guid reservationId, CancellationToken cancellationToken);
    }
}
