using AutoMapper;
using Users.Application.Abstractions.Mappings;
using Users.Domain;

namespace Users.Application.Dtos
{
    public class GetUserForExternalDto : IMapFrom<ApplicationUser>
    {
        public Guid ApplicationUserId { get; set; }
        public string Login { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool IsActive {  get; set; }
        public int[] Roles { get; set; } = default!;

        public void CreateMap(Profile profile)
        {
            profile.CreateMap<ApplicationUser, GetUserForExternalDto>()
                .ForMember(e => e.Roles, r =>
                    r.MapFrom(u => u.Roles.Select(s => s.ApplicationUserRoleId).ToArray()));
        }
    }
}
