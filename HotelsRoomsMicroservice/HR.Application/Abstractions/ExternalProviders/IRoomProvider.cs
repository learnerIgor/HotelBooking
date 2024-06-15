using HR.Domain;

namespace HR.Application.Abstractions.ExternalProviders
{
    public interface IRoomProvider
    {
        Task AddRoomAsync(string token, Room room, CancellationToken cancellationToken);
        Task UpdateRoomAsync(string token, string roomId, Room room, CancellationToken cancellationToken);
        Task DeleteRoomAsync(string token, Guid roomId, CancellationToken cancellationToken);
    }
}
