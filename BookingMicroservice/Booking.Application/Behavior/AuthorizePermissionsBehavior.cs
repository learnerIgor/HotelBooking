using Booking.Application.Abstractions.Service;
using Booking.Application.Exceptions;
using Booking.Domain.Enums;
using MediatR;

namespace Booking.Application.Behavior
{
    public class AuthorizePermissionsBehavior<TRequest, TResponse>(ICurrentUserService currentUserService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (currentUserService.CurrentUserId is null) throw new UnauthorizedException();

            if (currentUserService.CurrentUserRoles is null) throw new ForbiddenException();

            return next();
        }
    }
}