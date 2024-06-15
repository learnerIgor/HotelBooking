using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.RoomTypes.Queries.GetRoomTypes
{
    public class GetRoomTypesQueryValidator : AbstractValidator<GetRoomTypesQuery>
    {
        public GetRoomTypesQueryValidator()
        {
            RuleFor(e => e).IsValidListFilter();
            RuleFor(e => e).IsValidPaginationFilter();
        }
    }
}
