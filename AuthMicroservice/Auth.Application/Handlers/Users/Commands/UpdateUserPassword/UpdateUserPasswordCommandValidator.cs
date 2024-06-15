using FluentValidation;

namespace Auth.Application.Handlers.Users.Commands.UpdateUserPassword
{
    internal class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
    {
        public UpdateUserPasswordCommandValidator()
        {
            RuleFor(e => e.PasswordHash).MinimumLength(8).MaximumLength(100).NotEmpty();
        }
    }
}