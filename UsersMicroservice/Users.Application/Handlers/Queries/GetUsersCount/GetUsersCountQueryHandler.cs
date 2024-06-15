using Users.Application.Abstractions.Persistence.Repository.Read;
using Users.Application.BaseRealizations;
using Users.Domain;
using Users.Application.Caches;

namespace Users.Application.Handlers.Queries.GetUsersCount
{
    internal class GetUsersCountQueryHandler : BaseCashedQuery<GetUsersCountQuery, int>
    {
        private readonly IBaseReadRepository<ApplicationUser> _users;

        public GetUsersCountQueryHandler(IBaseReadRepository<ApplicationUser> users, ApplicationUsersCountMemoryCache cache) : base(cache)
        {
            _users = users;
        }
    
        public override async Task<int> SentQueryAsync(GetUsersCountQuery request, CancellationToken cancellationToken)
        {
            return await _users.AsAsyncRead().CountAsync(ListUserWhere.Where(request), cancellationToken);
        }
    }
}