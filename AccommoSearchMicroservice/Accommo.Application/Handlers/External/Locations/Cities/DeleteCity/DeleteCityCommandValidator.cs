using FluentValidation;

namespace Accommo.Application.Handlers.External.Locations.Cities.DeleteCity
{
    public class DeleteCityCommandValidator : AbstractValidator<DeleteCityCommand>
    {
        public DeleteCityCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty();
        }
    }
}
