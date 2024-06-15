using FluentValidation;
using HR.Application.Dtos;

namespace HR.Application.ValidatorsExtensions
{
    internal class CommonCommandValidator : AbstractValidator<CommonCommand>
    {
        public CommonCommandValidator()
        {
            RuleFor(e => e.Name).MaximumLength(50).When(e => e.Name is not null);
        }
    }

    public static class CommonCommandValidatorExtensions
    {
        internal static IRuleBuilderOptions<T, CommonCommand> IsValidCommonCommand<T>(this IRuleBuilder<T, CommonCommand> ruleBuilder)
        {
            return ruleBuilder
                .SetValidator(new CommonCommandValidator());
        }
    }
}