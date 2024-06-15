using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.Hotels.Commands.CreateHotel
{
    public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
    {
        public CreateHotelCommandValidator()
        {
            RuleFor(n => n).IsValidCommonCommand();
            RuleFor(d => d.Description).MinimumLength(10).MaximumLength(500).NotEmpty();
            RuleFor(d => d.Rating).InclusiveBetween(1, 5);
            RuleFor(a => a.Address).IsValidAddress();
            RuleFor(e => e).IsValidIBAN();
            RuleFor(e => e.Image).IsValidImageUrl().MaximumLength(50);
        }
    }
}
