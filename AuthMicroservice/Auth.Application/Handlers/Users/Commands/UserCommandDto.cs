using AutoMapper;
using Auth.Application.Abstractions.Mappings;
using Auth.Domain;

namespace Auth.Application.Handlers.Users.Commands
{
    public class UserCommandDto : IMapFrom<ApplicationUser>
    {
        public Guid ApplicationUserId { get; set; }
        public string Login { get; set; } = default!;
        public bool IsActive { get; set; }
        public string Email { get; set; } = default!;

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<ApplicationUser, UserCommandDto>();
        }
    }
}