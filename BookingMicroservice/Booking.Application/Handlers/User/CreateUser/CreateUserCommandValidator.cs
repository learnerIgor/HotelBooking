using Booking.Application.ValidatorsExtensions;
using FluentValidation;

namespace Booking.Application.Handlers.User.CreateUser
{
    internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(a => a.ApplicationUserId).IsGuid();
            RuleFor(e => e.Login).MinimumLength(3).MaximumLength(50).NotEmpty();
        }
    }
}
