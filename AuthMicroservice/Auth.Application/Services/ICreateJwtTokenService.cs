using Auth.Domain;

namespace Auth.Application.Services
{
    public interface ICreateJwtTokenService
    {
        string CreateJwtToken(ApplicationUser user, DateTime dateExpires);
    }
}
