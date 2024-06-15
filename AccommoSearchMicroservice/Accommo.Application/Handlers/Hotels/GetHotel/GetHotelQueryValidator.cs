using FluentValidation;
using Accommo.Application.ValidatorsExtensions;

namespace Accommo.Application.Handlers.Hotels.GetHotel
{
    public class GetHotelQueryValidator : AbstractValidator<GetHotelQuery>
    {
        public GetHotelQueryValidator()
        {
            RuleFor(i => i.Id).NotEmpty().IsGuid();
        }
    }
}
