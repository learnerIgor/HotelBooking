using FluentValidation;

namespace Accommo.Application.Handlers.External.RoomTypes.CreateRoomType
{
    public class CreateRoomTypeCommandValidator : AbstractValidator<CreateRoomTypeCommand>
    {
        public CreateRoomTypeCommandValidator()
        {
            RuleFor(b => b.BaseCost).NotEmpty().GreaterThan(0);
        }
    }
}
