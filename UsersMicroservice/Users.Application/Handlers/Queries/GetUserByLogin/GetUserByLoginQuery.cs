using MediatR;
using Users.Application.Dtos;

namespace Users.Application.Handlers.Queries.GetUserByLogin
{
    public class GetUserByLoginQuery : ListUserFilter, IRequest<GetUserForExternalDto>
    {
    }
}
