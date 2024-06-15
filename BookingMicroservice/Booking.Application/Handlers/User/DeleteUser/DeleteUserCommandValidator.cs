using FluentValidation;
using Booking.Application.ValidatorsExtensions;

namespace Booking.Application.Handlers.User.DeleteUser
{
    internal class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().IsGuid();
        }
    }
}