using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.Location.Cities.Commands.CreateCity
{
    public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidator()
        {
            RuleFor(n => n).IsValidCommonCommand();
        }
    }
}
