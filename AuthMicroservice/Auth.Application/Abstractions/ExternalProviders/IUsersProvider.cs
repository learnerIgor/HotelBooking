using Auth.Domain;

namespace Auth.Application.Abstractions.ExternalProviders
{
    public interface IUsersProvider
    {
        Task<ApplicationUser> GetUserAsync(string login, CancellationToken cancellationToken);
    }
}
