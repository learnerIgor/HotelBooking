namespace Users.Domain
{
    public class ApplicationUserApplicationUserRole
    {
        public Guid ApplicationUserId { get; private set; }
        public ApplicationUser ApplicationUser { get; private set; } = default!;

        public int ApplicationUserRoleId { get; private set; }
        public ApplicationUserRole Role { get; private set; } = default!;

        public ApplicationUserApplicationUserRole(int applicationUserRoleId) 
        {
            if(applicationUserRoleId <= 0)
            {
                throw new ArgumentException("ApplicationUserRoleId must be more than 0", nameof(applicationUserRoleId));
            }
            ApplicationUserRoleId = applicationUserRoleId;
        }
        private ApplicationUserApplicationUserRole() { }
    }
}
