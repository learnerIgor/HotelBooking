using Users.Domain.Enums;

namespace Users.Application.Abstractions.Attributes
{
    public class RequestAuthorizeAttribute(ApplicationUserRolesEnum[]? roles = null) : Attribute
    {
        public ApplicationUserRolesEnum[]? Roles { get; } = roles;
    }
}