using FluentValidation;
using Users.Application.ValidatorsExtensions;

namespace Users.Application.Handlers.Commands.DeleteUser;

internal class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(e => e.Id).NotEmpty().IsGuid();
    }
}