using HR.Domain;

namespace HR.Application.Abstractions.ExternalProviders
{
    public interface IRoomTypeProvider
    {
        Task AddRoomTypeAsync(string token, RoomType roomType, CancellationToken cancellationToken);
        Task UpdateRoomTypeAsync(string token, string roomTypeId, RoomType roomType, CancellationToken cancellationToken);
        Task UpdateCostRoomTypeAsync(string token, string roomTypeId, RoomType roomType, CancellationToken cancellationToken);
        Task DeleteRoomTypeAsync(string token, Guid roomTypeId, CancellationToken cancellationToken);
    }
}
