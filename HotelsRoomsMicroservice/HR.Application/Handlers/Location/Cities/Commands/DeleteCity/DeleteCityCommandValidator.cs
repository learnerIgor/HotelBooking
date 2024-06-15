using FluentValidation;

namespace HR.Application.Handlers.Location.Cities.Commands.DeleteCity
{
    public class DeleteCityCommandValidator : AbstractValidator<DeleteCityCommand>
    {
        public DeleteCityCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty();
        }
    }
}
