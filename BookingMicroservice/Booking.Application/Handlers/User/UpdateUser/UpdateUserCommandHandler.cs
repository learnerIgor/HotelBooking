using AutoMapper;
using Booking.Application.Exceptions;
using Booking.Application.Abstractions.Persistence.Repositories.Write;
using Booking.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Handlers.User.UpdateUser
{
    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, GetUserDto>
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

        public async Task<GetUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(request.Id);
            var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.ApplicationUserId == userId && e.IsActive, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException(request);
            }
            _mapper.Map(request, user);
            user.UpdateLogin(request.Login);
            user.UpdateEmail(request.Email);

            user = await _users.UpdateAsync(user, cancellationToken);
            var result = _mapper.Map<GetUserDto>(user);
            _logger.LogWarning($"User {user.ApplicationUserId} updated");

            return result;
        }
    }
}