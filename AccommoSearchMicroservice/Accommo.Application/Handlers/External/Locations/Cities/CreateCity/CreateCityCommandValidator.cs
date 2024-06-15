using FluentValidation;

namespace Accommo.Application.Handlers.External.Locations.Cities.CreateCity
{
    public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidator()
        {
            RuleFor(n => n.CityName).MaximumLength(50).NotEmpty();
            RuleFor(n => n.CountryName).MaximumLength(50).NotEmpty();
        }
    }
}
