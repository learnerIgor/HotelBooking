using Auth.Application.Abstractions.Persistence.Repositories.Write;
using Auth.Application.Exceptions;
using Auth.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Auth.Application.Handlers.Users.Commands.UpdateUserPassword
{
    internal class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand>
    {
        private readonly IBaseWriteRepository<ApplicationUser> _users;
        private readonly ILogger<UpdateUserPasswordCommandHandler> _logger;

        public UpdateUserPasswordCommandHandler(
            IBaseWriteRepository<ApplicationUser> users,
            ILogger<UpdateUserPasswordCommandHandler> logger)
        {
            _users = users;
            _logger = logger;
        }

        public async Task Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(request.UserId);
            var user = await _users.AsAsyncRead().SingleOrDefaultAsync(u => u.ApplicationUserId == userId && u.IsActive, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException(request);
            }
            user.UpdatePassword(request.PasswordHash);
            await _users.UpdateAsync(user, cancellationToken);

            _logger.LogWarning($"User password for {user.ApplicationUserId} updated");
        }
    }
}