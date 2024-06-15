using FluentValidation;

namespace Accommo.Application.Handlers.External.Hotels.UpdateHotel
{
    public class UpdateHotelCommandValidator : AbstractValidator<UpdateHotelCommand>
    {
        public UpdateHotelCommandValidator()
        {
            RuleFor(d => d.Description).MinimumLength(10).MaximumLength(500).NotEmpty();
            RuleFor(d => d.Rating).InclusiveBetween(1, 5);
        }
    }
}
