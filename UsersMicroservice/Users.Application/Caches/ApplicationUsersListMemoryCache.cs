using Users.Application.BaseRealizations;
using Users.Application.Dtos;

namespace Users.Application.Caches
{
    public class ApplicationUsersListMemoryCache : BaseCache<BaseListDto<GetUserDto>>;
}