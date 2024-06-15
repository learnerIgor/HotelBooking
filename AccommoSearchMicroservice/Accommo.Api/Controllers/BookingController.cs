using Accommo.Application.Handlers.External.Bookings;
using Accommo.Application.Handlers.External.Bookings.Commands.CreateBooking;
using Accommo.Application.Handlers.External.Bookings.Commands.DeleteBooking;
using Accommo.Application.Handlers.External.Bookings.Commands.UpdateBooking;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accommo.Api.Controllers
{
    /// <summary>
    /// BookingController
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        /// <summary>
        /// Create booking
        /// </summary>
        [HttpPost("/Booking")]
        public async Task<GetBookingDto> CreateBooking([FromBody] CreateBookingCommand command, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// Delete booking by id
        /// </summary>
        [HttpDelete("/Booking/{id}")]
        public Task DeleteBooking([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return mediator.Send(new DeleteBookingCommand { Id = id }, cancellationToken);
        }

        /// <summary>
        /// Update booking by id
        /// </summary>
        [HttpPut("/Booking/{id}")]
        public async Task<GetBookingDto> UpdateBooking([FromRoute] string id, [FromBody] UpdateBookingPayload payload, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new UpdateBookingCommand
            {
                ReservationId = id,
                StartDate = payload.CheckInDate,
                EndDate = payload.CheckOutDate,
            }, cancellationToken);
        }
    }
}
