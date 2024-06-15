using Booking.Application.ValidatorsExtensions;
using FluentValidation;

namespace Booking.Application.Handlers.User.UpdateUser
{
    internal class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().IsGuid();
            RuleFor(e => e.Login).MinimumLength(3).MaximumLength(50).NotEmpty();
            RuleFor(e => e.Email).MinimumLength(10).MaximumLength(50).NotEmpty();
        }
    }
}