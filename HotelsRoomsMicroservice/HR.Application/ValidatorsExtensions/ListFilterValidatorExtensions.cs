using FluentValidation;
using HR.Application.Handlers;

namespace HR.Application.ValidatorsExtensions
{
    internal class BaseListFilterValidator : AbstractValidator<ListFilter>
    {
        public BaseListFilterValidator()
        {
            RuleFor(e => e.FreeText).MaximumLength(50).When(e => e.FreeText is not null);
        }
    }

    public static class ListFilterValidatorExtensions
    {
        internal static IRuleBuilderOptions<T, ListFilter> IsValidListFilter<T>(this IRuleBuilder<T, ListFilter> ruleBuilder)
        {
            return ruleBuilder
                .SetValidator(new BaseListFilterValidator());
        }
    }
}