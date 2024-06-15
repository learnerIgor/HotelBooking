using FluentValidation;

namespace HR.Application.Handlers.Location.Countries.Commands.DeleteCountry
{
    public class DeleteCountryCommandValidator : AbstractValidator<DeleteCountryCommand>
    {
        public DeleteCountryCommandValidator()
        {
            RuleFor(e => e.Id).NotEmpty();
        }
    }
}
