using Auth.Application.Dtos;
using AutoMapper;
using Auth.Application.Abstractions.Persistence.Repositories.Read;
using Auth.Application.Exceptions;
using Auth.Application.Abstractions.Service;
using Auth.Domain;
using Auth.Application.BaseRealizations;
using Auth.Application.Caches;

namespace Auth.Application.Handlers.Users.Queries.GetCurrentUser
{
    internal class GetCurrentUserQueryHandler : BaseCashedForUserQuery<GetCurrentUserQuery, GetUserDto>
    {
        private readonly IBaseReadRepository<ApplicationUser> _users;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetCurrentUserQueryHandler(
            IBaseReadRepository<ApplicationUser> users,
            ICurrentUserService currentUserService,
            IMapper mapper,
            ApplicationUserMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
        {
            _users = users;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public override async Task<GetUserDto> SentQueryAsync(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _users.AsAsyncRead()
               .SingleOrDefaultAsync(e => e.ApplicationUserId == _currentUserService.CurrentUserId, cancellationToken);

            if (user is null)
            {
                throw new NotFoundException($"User with id {_currentUserService.CurrentUserId} not found");
            }

            return _mapper.Map<GetUserDto>(user);
        }
    }
}