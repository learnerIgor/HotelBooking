using HR.Domain.Enums;

namespace HR.Application.Abstractions.Service
{
    public interface ICurrentUserService
    {
        public Guid? CurrentUserId { get; }

        public ApplicationUserRolesEnum[] CurrentUserRoles { get; }

        public bool UserInRole(ApplicationUserRolesEnum role);

        public string Token { get; }
    }
}