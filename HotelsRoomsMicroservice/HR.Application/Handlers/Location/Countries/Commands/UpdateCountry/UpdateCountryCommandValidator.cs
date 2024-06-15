using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.Location.Countries.Commands.UpdateCountry
{
    public class UpdateCountryCommandValidator : AbstractValidator<UpdateCountryCommand>
    {
        public UpdateCountryCommandValidator()
        {
            RuleFor(n => n).IsValidCommonCommand();
        }
    }
}
