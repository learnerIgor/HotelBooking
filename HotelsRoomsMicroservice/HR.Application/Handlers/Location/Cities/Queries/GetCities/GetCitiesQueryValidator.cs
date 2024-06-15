using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.Location.Cities.Queries.GetCities
{
    public class GetCitiesQueryValidator : AbstractValidator<GetCitiesQuery>
    {
        public GetCitiesQueryValidator()
        {
            RuleFor(e => e).IsValidListFilter();
            RuleFor(e => e).IsValidPaginationFilter();
        }
    }
}
