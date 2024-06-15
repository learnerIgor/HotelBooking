using AutoMapper;
using Booking.Application.Abstractions.Mappings;
using Booking.Domain;

namespace Booking.Application.Handlers.Booking
{
    public class GetBookingDto : IMapFrom<Reservation>
    {
        public Guid ReservationId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public RoomDto Room { get; set; } = default!;
        public PaymentDto Payment { get; set; } = default!;


        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Reservation, GetBookingDto>()
                .ForMember(dest => dest.Room, opt => opt.MapFrom(src => src.Room))
                .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment));
        }
    }
}
