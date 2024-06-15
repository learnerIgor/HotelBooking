using Users.Application.Abstractions.Persistence.Repository.Writing;
using Users.Application.Abstractions.Service;
using Users.Application.Exceptions;
using Users.Application.Utils;
using Users.Domain;
using Users.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using Users.Application.Abstractions;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Users.Application.Handlers.Commands.UpdateUserPassword
{
    internal class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand>
    {
        private readonly IBaseWriteRepository<ApplicationUser> _users;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<UpdateUserPasswordCommandHandler> _logger;
        private readonly IMqService _mqService;

        public UpdateUserPasswordCommandHandler(
            IBaseWriteRepository<ApplicationUser> users, 
            ICurrentUserService currentUserService,
            ILogger<UpdateUserPasswordCommandHandler> logger,
            IMqService mqService)
        {
            _users = users;
            _currentUserService = currentUserService;
            _logger = logger;
            _mqService = mqService;
        }
    
        public async Task Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(request.UserId);
            var user = await _users.AsAsyncRead().SingleOrDefaultAsync(u => u.ApplicationUserId == userId && u.IsActive, cancellationToken);
        
            if (user is null)
            {
                throw new NotFoundException(request);
            }
        
            if (_currentUserService.CurrentUserId != userId &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException();
            }

            var newPasswordHash = PasswordHashUtil.Hash(request.Password);
            user.UpdatePassword(newPasswordHash, DateTime.UtcNow);
            await _users.UpdateAsync(user, cancellationToken);

            _mqService.SendUserMessage("updateUserPassword", JsonSerializer.Serialize(user, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            }));

            _logger.LogWarning($"User password for {user.ApplicationUserId.ToString()} updated by {_currentUserService.CurrentUserId.ToString()}");
        }
    }
}