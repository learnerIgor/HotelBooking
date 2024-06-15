using System.Security.Claims;
using Booking.Application.Abstractions.Service;
using Booking.Domain.Enums;

namespace Booking.Api.Services
{
    /// <summary>
    /// Current user service
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor for the current user's service
        /// </summary>
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get current user id
        /// </summary>
        public Guid? CurrentUserId
        {
            get
            {
                string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId is null)
                {
                    return null;
                }

                return Guid.Parse(userId);
            }
        }

        /// <summary>
        /// Check current user role
        /// </summary>
        public bool UserInRole(ApplicationUserRolesEnum role)
        {
            return CurrentUserRoles.Contains(role);
        }

        /// <summary>
        /// Get role of current user
        /// </summary>
        public ApplicationUserRolesEnum[] CurrentUserRoles => _httpContextAccessor.HttpContext!.User.Claims.Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .Select(Enum.Parse<ApplicationUserRolesEnum>)
            .ToArray();

        /// <summary>
        /// Get token of current user
        /// </summary>
        public string Token
        {
            get => _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
        }
    }
}