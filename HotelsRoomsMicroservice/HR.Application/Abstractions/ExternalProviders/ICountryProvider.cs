using HR.Domain;

namespace HR.Application.Abstractions.ExternalProviders
{
    public interface ICountryProvider
    {
        Task AddCountryAsync(string token, Country country, CancellationToken cancellationToken);
        Task UpdateCountryAsync(string token, string countryId, Country country, CancellationToken cancellationToken);
        Task DeleteCountryAsync(string token, Guid countryId, CancellationToken cancellationToken);
    }
}
