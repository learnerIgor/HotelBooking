using FluentValidation;
using Accommo.Application.ValidatorsExtensions;

namespace Accommo.Application.Handlers.External.Rooms.GetRoomById
{
    public class GetRoomByIdQueryValidator : AbstractValidator<GetRoomByIdQuery>
    {
        public GetRoomByIdQueryValidator()
        {
            RuleFor(i => i.Id).NotEmpty().IsGuid();
        }
    }
}
