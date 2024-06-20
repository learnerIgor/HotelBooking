using FluentValidation;
using Accommo.Application.ValidatorsExtensions;

namespace Accommo.Application.Handlers.External.Hotels.CreateHotel
{
    public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
    {
        public CreateHotelCommandValidator()
        {
            RuleFor(e => e.Name).MaximumLength(50).NotEmpty();
            RuleFor(d => d.Description).MinimumLength(10).MaximumLength(500).NotEmpty();
            RuleFor(d => d.Rating).InclusiveBetween(1, 5);
            RuleFor(a => a.Address).IsValidAddress();
            RuleFor(e => e.Image).IsValidImageUrl().MaximumLength(50);
        }
    }
}
