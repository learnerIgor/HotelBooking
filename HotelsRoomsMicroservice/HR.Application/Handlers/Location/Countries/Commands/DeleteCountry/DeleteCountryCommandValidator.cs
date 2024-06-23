using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.Location.Countries.Commands.DeleteCountry
{
    public class DeleteCountryCommandValidator : AbstractValidator<DeleteCountryCommand>
    {
        public DeleteCountryCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty().IsGuid();
        }
    }
}
