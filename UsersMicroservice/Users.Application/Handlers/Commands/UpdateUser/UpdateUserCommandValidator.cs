using Users.Application.ValidatorsExtensions;
using FluentValidation;

namespace Users.Application.Handlers.Commands.UpdateUser
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