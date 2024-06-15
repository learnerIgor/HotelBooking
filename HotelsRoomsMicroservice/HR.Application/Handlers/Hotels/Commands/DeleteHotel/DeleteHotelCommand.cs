using MediatR;

namespace HR.Application.Handlers.Hotels.Commands.DeleteHotel
{
    public class DeleteHotelCommand : IRequest<Unit>
    {
        public string Id { get; set; } = default!;
    }
}
