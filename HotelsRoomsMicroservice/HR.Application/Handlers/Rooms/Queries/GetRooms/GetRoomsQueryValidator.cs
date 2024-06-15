using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.Rooms.Queries.GetRooms
{
    public class GetRoomsQueryValidator : AbstractValidator<GetRoomsQuery>
    {
        public GetRoomsQueryValidator()
        {
            RuleFor(e => e).IsValidPaginationFilter();
            RuleFor(e => e.HotelId).IsGuid();
        }
    }
}
