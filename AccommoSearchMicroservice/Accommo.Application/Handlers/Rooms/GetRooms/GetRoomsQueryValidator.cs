using FluentValidation;
using Accommo.Application.ValidatorsExtensions;

namespace Accommo.Application.Handlers.Rooms.GetRooms
{
    public class GetRoomsQueryValidator : AbstractValidator<GetRoomsQuery>
    {
        public GetRoomsQueryValidator()
        {
            RuleFor(i => i.HotelId).NotEmpty().IsGuid();
            RuleFor(d => d.StartDate).IsDateTime().LessThan(d => d.EndDate);
            RuleFor(d => d.EndDate).IsDateTime().GreaterThan(d => d.StartDate);
            RuleFor(l => l).IsValidPaginationFilter();
        }
    }
}
