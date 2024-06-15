using AutoMapper;
using Users.Application.Abstractions.Persistence.Repository.Read;
using Users.Application.BaseRealizations;
using Users.Application.Exceptions;
using Users.Domain;
using Users.Domain.Enums;
using Users.Application.Caches;
using Users.Application.Dtos;
using Users.Application.Abstractions.Service;

namespace Users.Application.Handlers.Queries.GetUser
{
    internal class GetUserQueryHandler : BaseCashedQuery<GetUserQuery, GetUserDto>
    {
        private readonly IBaseReadRepository<ApplicationUser> _users;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
    

        public GetUserQueryHandler(IBaseReadRepository<ApplicationUser> users, IMapper mapper, ApplicationUserMemoryCache cache, ICurrentUserService currentUserService) : base(cache)
        {
            _users = users;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public override async Task<GetUserDto> SentQueryAsync(GetUserQuery request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            if (idGuid != _currentUserService.CurrentUserId &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException();
            }
            var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.ApplicationUserId == idGuid && e.IsActive, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException(request);
            }

            return _mapper.Map<GetUserDto>(user);
        }
    }
}