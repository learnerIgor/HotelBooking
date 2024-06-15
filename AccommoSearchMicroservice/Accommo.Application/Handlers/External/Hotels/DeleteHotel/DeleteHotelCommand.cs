using MediatR;

namespace Accommo.Application.Handlers.External.Hotels.DeleteHotel
{
    public class DeleteHotelCommand : IRequest<Unit>
    {
        public string Id { get; set; } = default!;
    }
}
