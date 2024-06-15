using Users.Domain.Enums;

namespace Users.Application.Abstractions.Service
{
    public interface ICurrentUserService
    {
        public Guid? CurrentUserId { get; }

        public ApplicationUserRolesEnum[] CurrentUserRoles { get; }

        public bool UserInRole(ApplicationUserRolesEnum role);
    }
}