using FluentValidation;
using Accommo.Application.ValidatorsExtensions;

namespace Accommo.Application.Handlers.External.RoomTypes.UpdateRoomTypeCost
{
    public class UpdateRoomTypeCostCommandValidator : AbstractValidator<UpdateRoomTypeCostCommand>
    {
        public UpdateRoomTypeCostCommandValidator()
        {
            RuleFor(b => b.Id).NotEmpty().IsGuid();
            RuleFor(b => b.BaseCost).NotEmpty().GreaterThan(0);
        }
    }
}
