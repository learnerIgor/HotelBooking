using Booking.Application.Handlers.Booking;
using Booking.Application.Handlers.Booking.Commands.CreateBooking;
using Booking.Application.Handlers.Booking.Commands.DeleteBooking;
using Booking.Application.Handlers.Booking.Commands.UpdateBooking;
using Booking.Application.Handlers.Booking.Queries.GetBooking;
using Booking.Application.Handlers.Booking.Queries.GetBookingsCount;
using Booking.Application.Handlers.Booking.Queries.GetUserBookings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
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
        /// Get bookig by id user 
        /// </summary>
        [HttpGet("/Bookigs/Users/{id}")]
        public async Task<GetBookingDto[]> GetUserBookings([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetUserBookingsQuery { ApplicationUserId = id }, cancellationToken);
            HttpContext.Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
            return result.Items;
        }

        /// <summary>
        /// Get bookig by id 
        /// </summary>
        [HttpGet("/Bookings/{id}")]
        public async Task<GetBookingDto> GetBooking([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetBookingQuery { ReservationId = id }, cancellationToken);
        }
        /// <summary>
        /// Get count bookigs 
        /// </summary>
        [HttpGet("/Count")]
        public async Task<int> GetBookingsCount([FromQuery] GetBookingsCountQuery query, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(query, cancellationToken);
        }

        /// <summary>
        /// Create booking
        /// </summary>
        [HttpPost("/Bookings")]
        public async Task<GetBookingDto> CreateBooking([FromBody] CreateBookingCommand command, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(command , cancellationToken);
        }

        /// <summary>
        /// Delete booking by id
        /// </summary>
        [HttpDelete("/Bookings/{id}")]
        public Task DeleteBooking([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return mediator.Send(new DeleteBookingCommand { Id = id }, cancellationToken);
        }

        /// <summary>
        /// Update booking by id
        /// </summary>
        [HttpPut("/Bookings/{id}")]
        public async Task<GetBookingDto> UpdateBooking([FromRoute] string id, [FromBody] UpdateBookingPayload payload, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new UpdateBookingCommand
            {
                ReservationId = id,
                StartDate = payload.StartDate,
                EndDate = payload.EndDate,
            }, cancellationToken);
        }
    }
}
