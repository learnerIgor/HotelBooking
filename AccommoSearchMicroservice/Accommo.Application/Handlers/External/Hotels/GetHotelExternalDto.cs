using Accommo.Application.Abstractions.Mappings;
using Accommo.Domain;
using AutoMapper;

namespace Accommo.Application.Handlers.External.Hotels
{
    public class GetHotelExternalDto : IMapFrom<Hotel>
    {
        public Guid HotelId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Rating { get; set; }
        public GetAddressExternalDto Address { get; set; } = default!;

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Hotel, GetHotelExternalDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .AfterMap((src, dest) =>
                {
                    if (dest.Address != null)
                    {
                        dest.Address.City = new GetCityExternalDto
                        {
                            Name = dest.Address.City.Name,
                            Country = new GetCountryExternalDto
                            {
                                Name = dest.Address.City.Country.Name
                            }
                        };
                    }
                });

            profile.CreateMap<Address, GetAddressExternalDto>();
            profile.CreateMap<City, GetCityExternalDto>();
            profile.CreateMap<Country, GetCountryExternalDto>();
        }
    }
}
