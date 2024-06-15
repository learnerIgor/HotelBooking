using Booking.Application.Abstractions.Mappings;
using Booking.Domain;
using MediatR;

namespace Booking.Application.Handlers.User.UpdateUser
{
    public class UpdateUserCommand : IRequest<GetUserDto>, IMapTo<ApplicationUser>
    {
        public string Id { get; init; } = default!;
        public string Login { get; init; } = default!;
        public string Email { get; init; } = default!;
    }
}