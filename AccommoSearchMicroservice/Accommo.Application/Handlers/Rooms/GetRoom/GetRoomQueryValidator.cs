using FluentValidation;
using Accommo.Application.ValidatorsExtensions;

namespace Accommo.Application.Handlers.Rooms.GetRoom
{
    public class GetRoomQueryValidator : AbstractValidator<GetRoomQuery>
    {
        public GetRoomQueryValidator()
        {
            RuleFor(i => i.Id).NotEmpty().IsGuid();
        }
    }
}
