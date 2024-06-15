namespace Auth.Domain
{
    public class ApplicationUser
    {
        public Guid ApplicationUserId { get; private set; }
        public string Login { get; private set; }
        public string PasswordHash { get; private set; }
        public bool IsActive { get; private set; }
        public IReadOnlyCollection<ApplicationUserRole> Roles { get; private set; }

        public ApplicationUser(Guid userId, string login, string password, ApplicationUserRole[] roles, bool isActive)
        {
            ApplicationUserId = userId;
            Login = login;
            PasswordHash = password;
            if (roles.Any())
            {

            }
            Roles = roles;
            IsActive = isActive;
        }

        public void UpdateIsActive(bool isActive)
        {
            IsActive = isActive;
        }

        public void UpdateLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentException("Login is empty", nameof(login));
            }
            if (login.Length > 50)
            {
                throw new ArgumentException("Login length more than 50", nameof(login));
            }
            if (login.Length < 3)
            {
                throw new ArgumentException("Login length less than 3", nameof(login));
            }

            Login = login;
        }

        public void UpdatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password is empty", nameof(password));
            }
            if (password.Length > 100)
            {
                throw new ArgumentException("Password length more than 100", nameof(password));
            }
            if (password.Length < 8)
            {
                throw new ArgumentException("Password length less than 8", nameof(password));
            }

            PasswordHash = password;
        }

        private ApplicationUser() { }
    }
}


