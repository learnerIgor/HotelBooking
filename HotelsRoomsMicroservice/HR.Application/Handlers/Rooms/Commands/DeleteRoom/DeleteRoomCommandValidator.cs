using FluentValidation;
using HR.Application.ValidatorsExtensions;

namespace HR.Application.Handlers.Rooms.Commands.DeleteRoom
{
    public class DeleteRoomCommandValidator : AbstractValidator<DeleteRoomCommand>
    {
        public DeleteRoomCommandValidator()
        {
            RuleFor(i => i.Id).NotEmpty().IsGuid();
        }
    }
}
