using Mail.Application.ValidatorsExtensions;
using FluentValidation;

namespace Mail.Application.Handlers.Commands.SendEmail
{
    public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
    {
        public SendEmailCommandValidator()
        {
            RuleFor(e => e.ApplicationUserId).NotEmpty().IsGuid();
            RuleFor(e => e.Email).MinimumLength(10).MaximumLength(50).NotEmpty().Matches("@");
        }
    }
}
