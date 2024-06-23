using Accommo.Application.ValidatorsExtensions;
using FluentValidation;

namespace Accommo.Application.Handlers.External.Locations.Countries.UpdateCountry
{
    public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
    {
        public UpdateCountryCommandValidator()
        {
            RuleFor(n => n.Id).NotEmpty().IsGuid();
            RuleFor(n => n.Name).MaximumLength(50).NotEmpty();
        }
    }
}
