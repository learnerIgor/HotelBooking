using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.Location.Cities.Queries.GetCity
{
    public class GetCityQueryValidator : AbstractValidator<GetCityQuery>
    {
        public GetCityQueryValidator()
        {
            RuleFor(i => i.Id).IsGuid().NotEmpty();
        }
    }
}
