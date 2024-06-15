using FluentValidation;

namespace Accommo.Application.Handlers.External.Hotels.CreateHotel
{
    public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
    {
        public CreateHotelCommandValidator()
        {
            RuleFor(d => d.Description).MinimumLength(10).MaximumLength(500).NotEmpty();
            RuleFor(d => d.Rating).InclusiveBetween(1, 5);
        }
    }
}
