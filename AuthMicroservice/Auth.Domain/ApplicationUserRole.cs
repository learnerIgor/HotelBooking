namespace Auth.Domain
{
    public class ApplicationUserRole
    {
        public int ApplicationUserRoleId { get; private set; }
        public string Name { get; private set; } = default!;
        public IEnumerable<ApplicationUser> Users { get; private set; } = default!;

        private ApplicationUserRole() { }
    }
}
