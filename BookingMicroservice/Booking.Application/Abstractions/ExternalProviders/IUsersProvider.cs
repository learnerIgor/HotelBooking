using Booking.Domain;

namespace Booking.Application.Abstractions.ExternalProviders
{
    public interface IRoomProvider
    {
        Task<Room> GetRoomAsync(string roomId, CancellationToken cancellationToken);
    }
}
