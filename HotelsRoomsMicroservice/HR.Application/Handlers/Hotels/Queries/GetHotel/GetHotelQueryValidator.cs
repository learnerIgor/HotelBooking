using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.Hotels.Queries.GetHotel
{
    public class GetHotelQueryValidator : AbstractValidator<GetHotelQuery>
    {
        public GetHotelQueryValidator()
        {
            RuleFor(i => i.Id).NotEmpty().IsGuid();
        }
    }
}
