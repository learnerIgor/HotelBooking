using FluentValidation;
using HR.Application.Dtos;
using System.Text.RegularExpressions;

namespace HR.Application.ValidatorsExtensions
{
    internal class IBANValidator : AbstractValidator<IAccountCommand>
    {
        static readonly string ibanPattern = @"^[A-Za-z]{2}[0-9]{2}[A-Za-z0-9]{4}[0-9]{7}([A-Za-z0-9]?){0,16}$";

        public IBANValidator()
        {
            RuleFor(i => i.IBAN)
            .Must(iban => Regex.IsMatch(iban, ibanPattern))
            .WithMessage("The IBAN does not match to the required form.");
        }
    }

    public static class IBANValidatorExtensions
    {
        internal static IRuleBuilderOptions<T, IAccountCommand> IsValidIBAN<T>(this IRuleBuilder<T, IAccountCommand> ruleBuilder)
        {
            return ruleBuilder
                .SetValidator(new IBANValidator());
        }
    }
}
