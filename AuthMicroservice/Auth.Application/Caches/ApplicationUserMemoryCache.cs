using Auth.Application.BaseRealizations;
using Auth.Application.Dtos;

namespace Auth.Application.Caches
{
    public class ApplicationUserMemoryCache : BaseCache<GetUserDto>;
}