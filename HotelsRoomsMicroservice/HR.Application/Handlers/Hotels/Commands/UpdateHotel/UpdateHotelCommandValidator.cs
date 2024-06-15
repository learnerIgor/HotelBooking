using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.Hotels.Commands.UpdateHotel
{
    public class UpdateHotelCommandValidator : AbstractValidator<UpdateHotelCommand>
    {
        public UpdateHotelCommandValidator()
        {
            RuleFor(i => i.Id).NotEmpty().IsGuid();
            RuleFor(n => n).IsValidCommonCommand();
            RuleFor(d => d.Description).MinimumLength(10).MaximumLength(500).NotEmpty();
            RuleFor(i => i).IsValidIBAN();
            RuleFor(d => d.Rating).InclusiveBetween(1, 5);
            RuleFor(a => a.Address).IsValidAddress();
            RuleFor(i => i.Image).IsValidImageUrl();
        }
    }
}
