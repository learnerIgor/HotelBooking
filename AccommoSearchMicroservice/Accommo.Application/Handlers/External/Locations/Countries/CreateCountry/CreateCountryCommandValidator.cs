using FluentValidation;

namespace Accommo.Application.Handlers.External.Locations.Countries.CreateCountry
{
    public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
    {
        public CreateCountryCommandValidator()
        {
            RuleFor(n => n.CountryId).NotEmpty();
            RuleFor(n => n.Name).MaximumLength(50).NotEmpty();
        }
    }
}
