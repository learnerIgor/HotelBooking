using Accommo.Domain.Enums;

namespace Accommo.Application.Abstractions.Service
{
    public interface ICurrentUserService
    {
        public Guid? CurrentUserId { get; }

        public ApplicationUserRolesEnum[] CurrentUserRoles { get; }

        public bool UserInRole(ApplicationUserRolesEnum role);
    }
}