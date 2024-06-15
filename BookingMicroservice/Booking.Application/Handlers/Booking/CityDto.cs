using AutoMapper;
using Booking.Application.Abstractions.Mappings;
using Booking.Domain;

namespace Booking.Application.Handlers.Booking
{
    public class CityDto : IMapFrom<City>
    {
        public string Name { get; set; } = default!;
        public CountryDto Country { get; set; } = default!;

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<City, CityDto>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));
        }
    }
}
