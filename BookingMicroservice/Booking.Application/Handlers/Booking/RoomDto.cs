using AutoMapper;
using Booking.Application.Abstractions.Mappings;
using Booking.Domain;

namespace Booking.Application.Handlers.Booking
{
    public class RoomDto : IMapFrom<Room>
    {
        public int Floor { get; set; }
        public int Number { get; set; }
        public string Image { get; set; } = default!;
        public RoomTypeDto RoomType { get; set; } = default!;
        public HotelDto Hotel { get; set; } = default!;

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Room, RoomDto>()
                .ForMember(dest => dest.RoomType, opt => opt.MapFrom(src => src.RoomType))
                .ForMember(dest => dest.Hotel, opt => opt.MapFrom(src => src.Hotel));
        }
    }
}
