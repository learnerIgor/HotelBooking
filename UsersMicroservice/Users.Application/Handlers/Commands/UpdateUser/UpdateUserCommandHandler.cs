using AutoMapper;
using Users.Application.Exceptions;
using Users.Application.Abstractions.Persistence.Repository.Writing;
using Users.Application.Abstractions.Service;
using Users.Domain;
using Users.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using Users.Application.Caches;
using Users.Application.Dtos;
using Users.Application.Abstractions;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Users.Application.Handlers.Commands.UpdateUser
{
    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, GetUserDto>
    {
        private readonly IBaseWriteRepository<ApplicationUser> _users;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ApplicationUsersListMemoryCache _applicationUsersListMemoryCache;
        private readonly ApplicationUsersCountMemoryCache _applicationUsersCountMemoryCache;
        private readonly ApplicationUserMemoryCache _applicationUserMemoryCache;
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private readonly IMqService _mqService;

        public UpdateUserCommandHandler(
            IBaseWriteRepository<ApplicationUser> users, 
            IMapper mapper,
            ICurrentUserService currentUserService,
            ApplicationUsersListMemoryCache applicationUsersListMemoryCache,
            ApplicationUsersCountMemoryCache applicationUsersCountMemoryCache,
            ApplicationUserMemoryCache applicationUserMemoryCache,
            ILogger<UpdateUserCommandHandler> logger,
            IMqService mqService)
        {
            _users = users;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _applicationUsersListMemoryCache = applicationUsersListMemoryCache;
            _applicationUsersCountMemoryCache = applicationUsersCountMemoryCache;
            _applicationUserMemoryCache = applicationUserMemoryCache;
            _logger = logger;
            _mqService = mqService;
        }

        public async Task<GetUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(request.Id);

            if (_currentUserService.CurrentUserId != userId &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException();
            }

            var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.ApplicationUserId == userId && e.IsActive, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException(request);
            }

            _mapper.Map(request, user);
            user.UpdateLogin(request.Login, DateTime.UtcNow);
            user.UpdateEmail(request.Email, DateTime.UtcNow);

            user = await _users.UpdateAsync(user, cancellationToken);
            var result = _mapper.Map<GetUserDto>(user);
        
            _applicationUsersListMemoryCache.Clear();
            _applicationUsersCountMemoryCache.Clear();
            _applicationUserMemoryCache.Set(new GetUserDto {ApplicationUserId = user.ApplicationUserId}, result, 1);
            _logger.LogWarning($"User {user.ApplicationUserId} updated by {_currentUserService.CurrentUserId}");

            _mqService.SendMessageToExchange("updateUser", JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            }));

            return result;
        }
    }
}