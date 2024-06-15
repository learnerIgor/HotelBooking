using HR.Domain;

namespace HR.Application.Abstractions.ExternalProviders
{
    public interface IHotelProvider
    {
        Task AddHotelAsync(string token, Hotel hotel, CancellationToken cancellationToken);
        Task UpdateHotelAsync(string token, string hotelId, Hotel hotel, CancellationToken cancellationToken);
        Task DeleteHotelAsync(string token, Guid hotelId, CancellationToken cancellationToken);
    }
}
