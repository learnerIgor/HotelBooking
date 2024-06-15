using AutoMapper;
using HR.Application.Abstractions.Mappings;
using HR.Domain;

namespace HR.Application.Handlers.Location.Cities
{
    public class GetCityDto : IMapFrom<City>
    {
        public Guid CityId { get; set; }
        public string Name { get; set; } = default!;


        public void CreateMap(Profile profile)
        {
            profile.CreateMap<City, GetCityDto>();
        }
    }
}
