using FluentValidation;

namespace Auth.Application.Handlers.Auth.Commands.CreateJwtToken
{
    public class CreateJwtTokenCommandValidator : AbstractValidator<CreateJwtTokenCommand>
    {
        public CreateJwtTokenCommandValidator()
        {
            RuleFor(e => e.Login).MinimumLength(5).MaximumLength(50).NotEmpty();
            RuleFor(e => e.Password).MinimumLength(8).MaximumLength(50).NotEmpty();
        }
    }
}
