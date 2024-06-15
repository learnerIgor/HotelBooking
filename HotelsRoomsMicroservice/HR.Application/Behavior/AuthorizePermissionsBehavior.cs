using HR.Application.Abstractions.Service;
using HR.Application.Exceptions;
using HR.Domain.Enums;
using MediatR;

namespace HR.Application.Behavior
{
    public class AuthorizePermissionsBehavior<TRequest, TResponse>(ICurrentUserService currentUserService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (currentUserService.CurrentUserId is null) throw new UnauthorizedException();

            if (currentUserService.CurrentUserRoles is null || !currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException();
            }

            return next();
        }
    }
}