using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.Rooms.Queries.GetRoom
{
    public class GetRoomQueryValidator : AbstractValidator<GetRoomQuery>
    {
        public GetRoomQueryValidator()
        {
            RuleFor(i => i.Id).NotEmpty().IsGuid();
        }
    }
}
