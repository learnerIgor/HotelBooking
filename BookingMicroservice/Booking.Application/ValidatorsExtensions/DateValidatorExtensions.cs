using FluentValidation;

namespace Booking.Application.ValidatorsExtensions
{
    public static class DateValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> IsDateTime<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(e => DateTime.TryParse(e, out _)).WithErrorCode("Not a date");
        }
    }
}
