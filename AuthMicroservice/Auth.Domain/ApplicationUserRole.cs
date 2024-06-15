namespace Auth.Domain
{
    public class ApplicationUserRole
    {
        public int ApplicationUserRoleId { get; set; }
        public string Name { get; set; } = default!;
        public IEnumerable<ApplicationUser> Users { get; set; } = default!;

        private ApplicationUserRole() { }
    }
}
