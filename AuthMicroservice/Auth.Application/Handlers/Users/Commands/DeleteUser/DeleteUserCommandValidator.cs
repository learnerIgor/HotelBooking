using FluentValidation;
using Auth.Application.ValidatorsExtensions;

namespace Auth.Application.Handlers.Users.Commands.DeleteUser
{
    internal class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().IsGuid();
        }
    }
}