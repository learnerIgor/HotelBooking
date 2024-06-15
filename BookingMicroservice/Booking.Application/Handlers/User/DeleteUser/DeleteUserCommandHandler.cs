using Booking.Application.Exceptions;
using Booking.Application.Abstractions.Persistence.Repositories.Write;
using Booking.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Handlers.User.DeleteUser
{
    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IBaseWriteRepository<ApplicationUser> _users;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(
            IBaseWriteRepository<ApplicationUser> users,
            ILogger<DeleteUserCommandHandler> logger)
        {
            _users = users;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(request.Id);
            var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.ApplicationUserId == userId, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException(request);
            }
            user.UpdateIsActive(false);
            await _users.UpdateAsync(user, cancellationToken);
            _logger.LogWarning($"User {user.ApplicationUserId} deleted");

            return default;
        }
    }
}