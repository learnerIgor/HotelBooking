using FluentValidation;
using Accommo.Application.ValidatorsExtensions;

namespace Accommo.Application.Handlers.External.Rooms.DeleteRoom
{
    public class DeleteRoomCommandValidator : AbstractValidator<DeleteRoomCommand>
    {
        public DeleteRoomCommandValidator()
        {
            RuleFor(i => i.Id).NotEmpty().IsGuid();
        }
    }
}
