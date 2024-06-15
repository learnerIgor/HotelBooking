using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Booking.Application.Abstractions.Persistence.Repositories.Write;
using Booking.Application.Exceptions;
using Booking.Domain;

namespace Booking.Application.Handlers.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GetUserDto>
    {
        private readonly IBaseWriteRepository<ApplicationUser> _users;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(
            IBaseWriteRepository<ApplicationUser> users,
            IMapper mapper,
            ILogger<CreateUserCommandHandler> logger)
        {
            _users = users;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.Login == request.Login && e.IsActive, cancellationToken);
            if (userExist != null)
            {
                throw new BadOperationException($"User with login {request.Login} already exists.");
            }

            var idUserGuid = Guid.Parse(request.ApplicationUserId);
            var user = new ApplicationUser(idUserGuid, request.Login, request.Email,request.IsActive);
            user = await _users.AddAsync(user, cancellationToken);
            _logger.LogInformation($"New user {user.ApplicationUserId} created.");

            return _mapper.Map<GetUserDto>(user);
        }
    }
}

