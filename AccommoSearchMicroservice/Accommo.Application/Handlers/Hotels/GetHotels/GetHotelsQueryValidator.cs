using Accommo.Application.ValidatorsExtensions;
using FluentValidation;

namespace Accommo.Application.Handlers.Hotels.GetHotels
{
    public class GetHotelsQueryValidator : AbstractValidator<GetHotelsQuery>
    {
        public GetHotelsQueryValidator()
        {
            RuleFor(e => e.LocationText).MaximumLength(50).NotEmpty();
            RuleFor(d => d.StartDate).IsDateTime().LessThan(d => d.EndDate);
            RuleFor(d => d.EndDate).IsDateTime().GreaterThan(d => d.StartDate);
            RuleFor(l => l).IsValidPaginationFilter();
        }
    }
}
