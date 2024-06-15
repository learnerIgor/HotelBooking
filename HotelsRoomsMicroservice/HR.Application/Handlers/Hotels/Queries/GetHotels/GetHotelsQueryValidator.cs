using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.Hotels.Queries.GetHotels
{
    public class GetHotelsQueryValidator : AbstractValidator<GetHotelsQuery>
    {
        public GetHotelsQueryValidator()
        {
            RuleFor(e => e).IsValidListFilter();
            RuleFor(e => e).IsValidPaginationFilter();
        }
    }
}
