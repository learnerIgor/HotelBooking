using System.Reflection;
using Users.Application.Abstractions.Service;
using Users.Application.Exceptions;
using MediatR;
using Users.Application.Abstractions.Attributes;

namespace Users.Application.Behavior
{
    public class AuthorizePermissionsBehavior<TRequest, TResponse>(ICurrentUserService currentUserService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (request.GetType().GetCustomAttribute(typeof(RequestAuthorizeAttribute), true) is not RequestAuthorizeAttribute requestAuthorizeAttribute)
            {
                return next();
            }

            if (currentUserService.CurrentUserId is null) throw new UnauthorizedException();

            if (requestAuthorizeAttribute.Roles is null || requestAuthorizeAttribute.Roles.Length == 0)
            {
                return next();
            }

            if (currentUserService.CurrentUserRoles is null)
            {
                throw new ForbiddenException();
            }

            var requiredRoles = requestAuthorizeAttribute.Roles;
            if (requiredRoles == null || requiredRoles.Length == 0)
            {
                return next();
            }

            if (requiredRoles.Any(rn => currentUserService.CurrentUserRoles.All(role => role != rn)))
            {
                throw new ForbiddenException();
            }

            return next();
        }
    }
}