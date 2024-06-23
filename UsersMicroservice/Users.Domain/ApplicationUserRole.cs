namespace Users.Domain
{
    public class ApplicationUserRole
    {
        public int ApplicationUserRoleId { get; private set; }
        public string Name { get; private set; } = default!;
        public IEnumerable<ApplicationUserApplicationUserRole> Users { get; private set; } = default!;
    }
}
