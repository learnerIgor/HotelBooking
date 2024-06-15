using FluentValidation;

namespace Accommo.Application.Handlers.External.Locations.Cities.UpdateCity
{
    public class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
    {
        public UpdateCityCommandValidator()
        {
            RuleFor(n => n.Name).MaximumLength(50).NotEmpty();
        }
    }
}
