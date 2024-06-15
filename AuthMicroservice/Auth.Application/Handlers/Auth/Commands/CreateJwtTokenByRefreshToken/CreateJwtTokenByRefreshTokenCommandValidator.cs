using Auth.Application.ValidatorsExtensions;
using FluentValidation;

namespace Auth.Application.Handlers.Auth.Commands.CreateJwtTokenByRefreshToken
{
    internal class CreateJwtTokenByRefreshTokenCommandValidator : AbstractValidator<CreateJwtTokenByRefreshTokenCommand>
    {
        public CreateJwtTokenByRefreshTokenCommandValidator()
        {
            RuleFor(e => e.RefreshToken).NotEmpty().IsGuid();
        }
    }
}