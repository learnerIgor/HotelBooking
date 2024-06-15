using AutoMapper;
using HR.Domain;
using HR.Application.Abstractions.Mappings;

namespace HR.Application.Handlers.Location.Countries
{
    public class GetCountryDto : IMapFrom<Country>
    {
        public Guid CountryId { get; set; }
        public string Name { get; set; } = default!;

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<Country, GetCountryDto>();
        }
    }
}
