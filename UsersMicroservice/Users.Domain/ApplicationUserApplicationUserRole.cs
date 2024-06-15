namespace Users.Domain
{
    public class ApplicationUserApplicationUserRole
    {
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = default!;

        public int ApplicationUserRoleId { get; set; }
        public ApplicationUserRole Role { get; set; } = default!;

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
