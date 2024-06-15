using AutoMapper;
using Booking.Application.Abstractions.Mappings;
using Booking.Domain;

namespace Booking.Application.Handlers.Booking
{
    public class HotelDto : IMapFrom<Hotel>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Rating { get; set; }
        public string Image { get; set; } = default!;
        public AddressDto Address { get; set; } = default!;

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Hotel, HotelDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
        }
    }
}
