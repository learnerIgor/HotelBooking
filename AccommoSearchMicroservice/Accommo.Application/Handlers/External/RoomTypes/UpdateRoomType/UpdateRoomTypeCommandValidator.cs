using FluentValidation;

namespace Accommo.Application.Handlers.External.RoomTypes.UpdateRoomType
{
    public class UpdateRoomTypeCommandValidator : AbstractValidator<UpdateRoomTypeCommand>
    {
        public UpdateRoomTypeCommandValidator()
        {
            RuleFor(e => e.Name).MaximumLength(50).When(e => e.Name is not null);
        }
    }
}
