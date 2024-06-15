using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.RoomTypes.Commands.CreateRoomType
{
    public class CreateRoomTypeCommandValidator : AbstractValidator<CreateRoomTypeCommand>
    {
        public CreateRoomTypeCommandValidator()
        {
            RuleFor(n => n).IsValidCommonCommand();
            RuleFor(b => b.BaseCost).NotEmpty().GreaterThan(0);
        }
    }
}
