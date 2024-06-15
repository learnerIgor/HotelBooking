using AutoMapper;
using Users.Application.Abstractions.Mappings;
using Users.Domain;

namespace Users.Application.Handlers.Commands
{
    public class GetUserMqDto : IMapFrom<ApplicationUser>
    {
        public string ApplicationUserId { get; set; } = default!;
        public string Login { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool IsActive { get; set; }
        public int[] Roles { get; set; }

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<ApplicationUser, GetUserMqDto>()
                .ForMember(e => e.Roles, r =>
                    r.MapFrom(u => u.Roles.Select(s => s.ApplicationUserRoleId).ToArray()));
        }
    }
}
