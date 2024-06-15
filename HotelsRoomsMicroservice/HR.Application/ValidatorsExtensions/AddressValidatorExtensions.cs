using FluentValidation;
using HR.Application.Handlers.Hotels;

namespace HR.Application.ValidatorsExtensions
{
    internal sealed class BaseAddressValidator : AbstractValidator<AddressDto>
    {
        public BaseAddressValidator()
        {
            RuleFor(a => a.Street).MinimumLength(3).MaximumLength(50).NotEmpty();
            RuleFor(a => a.HouseNumber).MinimumLength(1).MaximumLength(10).NotEmpty();
            RuleFor(a => a.Latitude).NotNull().InclusiveBetween(-90, 90);
            RuleFor(a => a.Longitude).NotNull().InclusiveBetween(-180, 180);
            RuleFor(a => a.City.Name).MinimumLength(3).MaximumLength(50).NotEmpty();
            RuleFor(a => a.City.Country.Name).MinimumLength(3).MaximumLength(50).NotEmpty();
        }
    }

    public static class AddressValidatorExtensions
    {
        public static void IsValidAddress<T>(this IRuleBuilder<T, AddressDto> ruleBuilder)
        {
            ruleBuilder
                .SetValidator(new BaseAddressValidator());
        }
    }
}
