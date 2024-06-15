using AutoMapper;
using Auth.Application.Exceptions;
using Auth.Application.Abstractions.Persistence.Repositories.Write;
using Auth.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Auth.Application.Handlers.Users.Commands.UpdateUser
{
    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserCommandDto>
    {
        private readonly IBaseWriteRepository<ApplicationUser> _users;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(
            IBaseWriteRepository<ApplicationUser> users,
            IMapper mapper,
            ILogger<UpdateUserCommandHandler> logger)
        {
            _users = users;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserCommandDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(request.Id);
            var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.ApplicationUserId == userId && e.IsActive, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException(request);
            }
            _mapper.Map(request, user);
            user.UpdateLogin(request.Login);

            user = await _users.UpdateAsync(user, cancellationToken);
            var result = _mapper.Map<UserCommandDto>(user);
            _logger.LogWarning($"User {user.ApplicationUserId} updated");

            return result;
        }
    }
}