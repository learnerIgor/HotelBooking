namespace Auth.ExternalProviders.Models
{
    public class GetUserDto
    {
        public Guid ApplicationUserId { get; set; }
        public string Login { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public bool IsActive {  get; set; }
        public int[] Roles { get; set; } = default!;
    }
}