using AutoMapper;
using Booking.Application.Abstractions.Mappings;
using Booking.Domain;

namespace Booking.Application.Handlers.Booking
{
    public class CountryDto : IMapFrom<Country>
    {
        public string Name { get; set; } = default!;

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Country, CountryDto>();
        }
    }
}
