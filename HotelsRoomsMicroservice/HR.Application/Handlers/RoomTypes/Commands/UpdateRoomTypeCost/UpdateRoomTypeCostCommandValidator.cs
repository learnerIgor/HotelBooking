using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.RoomTypes.Commands.UpdateRoomTypeCost
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
