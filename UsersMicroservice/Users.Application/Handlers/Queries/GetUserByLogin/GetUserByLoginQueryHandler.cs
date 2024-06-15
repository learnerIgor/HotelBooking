using AutoMapper;
using Users.Application.Abstractions.Persistence.Repository.Read;
using Users.Application.BaseRealizations;
using Users.Application.Caches;
using Users.Application.Dtos;
using Users.Application.Exceptions;
using Users.Domain;

namespace Users.Application.Handlers.Queries.GetUserByLogin
{
    public class GetUserByLoginQueryHandler : BaseCashedQuery<GetUserByLoginQuery, GetUserForExternalDto>
    {
        private readonly IBaseReadRepository<ApplicationUser> _users;
        private readonly IMapper _mapper;
        public GetUserByLoginQueryHandler(IBaseReadRepository<ApplicationUser> users, IMapper mapper, ApplicationUserExternalMemoryCache cache) : base(cache)
        {
            _users = users;
            _mapper = mapper;
        }

        public override async Task<GetUserForExternalDto> SentQueryAsync(GetUserByLoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _users.AsAsyncRead().SingleOrDefaultAsync(e => e.Login == request.FreeText && e.IsActive, cancellationToken);
            if (user is null)
            {
                throw new NotFoundException(request);
            }

            return _mapper.Map<GetUserForExternalDto>(user);
        }
    }
}
