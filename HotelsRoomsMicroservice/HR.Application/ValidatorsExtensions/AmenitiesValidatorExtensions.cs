using FluentValidation;
using HR.Application.Handlers.Rooms;

namespace HR.Application.ValidatorsExtensions
{
    internal sealed class BaseAmenitiesValidator : AbstractValidator<Amenities>
    {
        public BaseAmenitiesValidator()
        {
            RuleFor(r => r.Bed).Must(x => x == false || x == true);
            RuleFor(r => r.Chair).Must(x => x == false || x == true);
            RuleFor(r => r.Table).Must(x => x == false || x == true);
            RuleFor(r => r.BedsideTable).Must(x => x == false || x == true);
            RuleFor(r => r.Wardrobe).Must(x => x == false || x == true);
            RuleFor(r => r.Balcony).Must(x => x == false || x == true);
            RuleFor(r => r.WiFi).Must(x => x == false || x == true);
            RuleFor(r => r.TV).Must(x => x == false || x == true);
            RuleFor(r => r.AirConditioner).Must(x => x == false || x == true);
            RuleFor(r => r.Phone).Must(x => x == false || x == true);
            RuleFor(r => r.Bar).Must(x => x == false || x == true);
            RuleFor(r => r.Safe).Must(x => x == false || x == true);
            RuleFor(r => r.Food).Must(x => x == false || x == true);
            RuleFor(r => r.NonSmoking).Must(x => x == false || x == true);
            RuleFor(r => r.Smoking).Must(x => x == false || x == true);
            RuleFor(r => r.Pets).Must(x => x == false || x == true);
            RuleFor(r => r.PrivateBathroom).Must(x => x == false || x == true);
            RuleFor(r => r.SeaView).Must(x => x == false || x == true);
            RuleFor(r => r.HydromassageBath).Must(x => x == false || x == true);
            RuleFor(r => r.Terrace).Must(x => x == false || x == true);
            RuleFor(r => r.Sofa).Must(x => x == false || x == true);
            RuleFor(r => r.Dishwasher).Must(x => x == false || x == true);
            RuleFor(r => r.Bath).Must(x => x == false || x == true);
            RuleFor(r => r.Soundproofing).Must(x => x == false || x == true);
            RuleFor(r => r.Refrigerator).Must(x => x == false || x == true);
            RuleFor(r => r.IroningAccessories).Must(x => x == false || x == true);
            RuleFor(r => r.Shower).Must(x => x == false || x == true);
            RuleFor(r => r.WashingMachine).Must(x => x == false || x == true);
            RuleFor(r => r.Toilet).Must(x => x == false || x == true);
            RuleFor(r => r.Pool).Must(x => x == false || x == true);
        }
    }
    public static class AmenitiesValidatorExtensions
    {
        public static void IsValidAmenities<T>(this IRuleBuilder<T, Amenities> ruleBuilder)
        {
            ruleBuilder
                .SetValidator(new BaseAmenitiesValidator());
        }
    }
}
