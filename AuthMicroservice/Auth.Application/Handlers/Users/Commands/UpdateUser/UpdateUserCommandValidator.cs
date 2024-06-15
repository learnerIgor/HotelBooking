using Auth.Application.ValidatorsExtensions;
using FluentValidation;

namespace Auth.Application.Handlers.Users.Commands.UpdateUser
{
    internal class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().IsGuid();
            RuleFor(e => e.Login).MinimumLength(3).MaximumLength(50).NotEmpty();
        }
    }
}