using Users.Application.Exceptions;
using Users.Application.Abstractions.Persistence.Repository.Writing;
using Users.Domain;
using Users.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using Users.Application.Caches;
using Users.Application.Abstractions.Service;
using Users.Application.Handlers.Queries.GetUser;
using Users.Application.Abstractions;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Users.Application.Handlers.Commands.DeleteUser
{
    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IBaseWriteRepository<ApplicationUser> _users;
        private readonly ICurrentUserService _currentUserService;
        private readonly ApplicationUsersListMemoryCache _listCache;
        private readonly ApplicationUsersCountMemoryCache _countCache;
        private readonly ILogger<DeleteUserCommandHandler> _logger;
        private readonly ApplicationUserMemoryCache _userCache;
        private readonly IMqService _mqService;

        public DeleteUserCommandHandler(
            IBaseWriteRepository<ApplicationUser> users, 
            ICurrentUserService currentUserService, 
            ApplicationUsersListMemoryCache listCache,
            ApplicationUsersCountMemoryCache countCache,
            ILogger<DeleteUserCommandHandler> logger,
            ApplicationUserMemoryCache userCache,
            IMqService mqService)
        {
            _users = users;
            _currentUserService = currentUserService;
            _listCache = listCache;
            _countCache = countCache;
            _logger = logger;
            _userCache = userCache;
            _mqService = mqService;
        }
    
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(request.Id);
        
            if (userId != _currentUserService.CurrentUserId &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException();
            }

            var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.ApplicationUserId == userId, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException(request);
            }

            user.UpdateIsActive(false);

            await _users.UpdateAsync(user, cancellationToken);
            _listCache.Clear();
            _countCache.Clear();
            _logger.LogWarning($"User {user.ApplicationUserId} deleted by {_currentUserService.CurrentUserId}");
            _userCache.DeleteItem(new GetUserQuery {Id = user.ApplicationUserId.ToString()});

            _mqService.SendMessageToExchange("deleteUser", JsonSerializer.Serialize(request.Id, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            }));

            return default;
        }
    }
}