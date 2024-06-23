using Accommo.Application.ValidatorsExtensions;
using FluentValidation;

namespace Accommo.Application.Handlers.External.Locations.Countries.DeleteCountry
{
    public class DeleteCountryCommandValidator : AbstractValidator<DeleteCountryCommand>
    {
        public DeleteCountryCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().IsGuid();
        }
    }
}
