using HR.Domain;

namespace HR.Application.Abstractions.ExternalProviders
{
    public interface ICityProvider
    {
        Task AddCityAsync(string token, string countryName, City city, CancellationToken cancellationToken);
        Task UpdateCityAsync(string token, string cityId, City city, CancellationToken cancellationToken);
        Task DeleteCityAsync(string token, Guid cityId, CancellationToken cancellationToken);
    }
}
