using AutoMapper;
using Booking.Application.Abstractions.Mappings;
using Booking.Domain;

namespace Booking.Application.Handlers.Booking
{
    public class AddressDto : IMapFrom<Address>
    {
        public string Street { get; set; } = default!;
        public string HouseNumber { get; set; } = default!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public CityDto City { get; set; } = default!;

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Address, AddressDto>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City));
        }
    }
}
