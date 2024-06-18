using FluentValidation;

namespace Users.Application.Handlers.Commands.CreateUser
{
    internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(e => e.Login).MinimumLength(5).MaximumLength(50).NotEmpty();
            RuleFor(e => e.Password).MinimumLength(8).MaximumLength(100).NotEmpty();
            RuleFor(e => e.Email).MinimumLength(10).MaximumLength(50).NotEmpty().Matches("@");
        }
    }
}
