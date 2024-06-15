using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;
using Users.Application.Abstractions;
using Users.Application.Abstractions.Persistence.Repository.Writing;
using Users.Application.Caches;
using Users.Application.Dtos;
using Users.Application.Exceptions;
using Users.Application.Utils;
using Users.Domain;
using Users.Domain.Enums;

namespace Users.Application.Handlers.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GetUserDto>
    {
        private readonly IBaseWriteRepository<ApplicationUser> _users;
        private readonly IMapper _mapper;
        private readonly ApplicationUsersListMemoryCache _listCache;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly ApplicationUsersCountMemoryCache _countCache;
        private readonly IMqService _mqService;

        public CreateUserCommandHandler(IBaseWriteRepository<ApplicationUser> users, IMapper mapper,
        ApplicationUsersListMemoryCache listCache,
        ILogger<CreateUserCommandHandler> logger,
        ApplicationUsersCountMemoryCache countCache,
        IMqService mqService)
        {
            _users = users;
            _mapper = mapper;
            _listCache = listCache;
            _logger = logger;
            _countCache = countCache;
            _mqService = mqService;
        }

        public async Task<GetUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.Login == request.Login && e.IsActive, cancellationToken);
            if (userExist != null)
            {
                throw new BadOperationException($"User with login {request.Login} already exists.");
            }

            var user = new ApplicationUser(request.Login, PasswordHashUtil.Hash(request.Password), request.Email, DateTime.UtcNow, [new ApplicationUserApplicationUserRole((int)ApplicationUserRolesEnum.Client)], true);
            user = await _users.AddAsync(user, cancellationToken);
            _listCache.Clear();
            _countCache.Clear();
            _logger.LogInformation($"New user {user.ApplicationUserId} created.");

            _mqService.SendUserMessage("addUser", JsonSerializer.Serialize(_mapper.Map<GetUserMqDto>(user), new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            }));

            return _mapper.Map<GetUserDto>(user);
        }
    }
}

