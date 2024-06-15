using FluentValidation;

namespace Accommo.Application.Handlers.External.Locations.Countries.UpdateCountry
{
    public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
    {
        public UpdateCountryCommandValidator()
        {
            RuleFor(n => n.Name).MaximumLength(50).NotEmpty();
        }
    }
}
