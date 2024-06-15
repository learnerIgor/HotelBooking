using Accommo.Application.Abstractions.Mappings;
using Accommo.Domain;
using AutoMapper;

namespace Accommo.Application.Handlers.External.Bookings
{
    public class GetBookingDto : IMapFrom<Reservation>
    {
        public Guid ReservationId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public bool IsActive { get; set; }
        public Guid ApplicationUserId { get; set; }
        public Guid RoomId { get; set; }

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Reservation, GetBookingDto>();
        }
    }
}
