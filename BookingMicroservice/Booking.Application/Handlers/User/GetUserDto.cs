using AutoMapper;
using Booking.Application.Abstractions.Mappings;
using Booking.Domain;

namespace Booking.Application.Handlers.User
{
    public class GetUserDto : IMapFrom<ApplicationUser>
    {
        public Guid ApplicationUserId { get; set; }
        public string Login { get; set; } = default!;
        public bool IsActive { get; set; }
        public string Email { get; set; } = default!;

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<ApplicationUser, GetUserDto>();
        }
    }
}