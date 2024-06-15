using Booking.Domain.Enums;

namespace Booking.Application.Abstractions.Service
{
    public interface ICurrentUserService
    {
        public Guid? CurrentUserId { get; }

        public ApplicationUserRolesEnum[] CurrentUserRoles { get; }

        public bool UserInRole(ApplicationUserRolesEnum role);

        public string Token { get; }
    }
}