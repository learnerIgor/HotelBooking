namespace Users.Domain
{
    public class ApplicationUser
    {
        public Guid ApplicationUserId { get; set; } = default!;
        public string Login { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public IEnumerable<ApplicationUserApplicationUserRole> Roles { get; set; } =
            new List<ApplicationUserApplicationUserRole>();

        public ApplicationUser(string login, string passwordHash, string email, DateTime createdDate, IEnumerable<ApplicationUserApplicationUserRole> roles, bool isActive)
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
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is empty", nameof(email));
            }
            if (email.Length > 50)
            {
                throw new ArgumentException("Email length more than 50", nameof(email));
            }
            if (email.Length < 10)
            {
                throw new ArgumentException("Email length less than 10", nameof(email));
            }
            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException("Password is empty", nameof(passwordHash));
            }
            if (passwordHash.Length > 100)
            {
                throw new ArgumentException("Password length more than 100", nameof(passwordHash));
            }
            if (passwordHash.Length < 8)
            {
                throw new ArgumentException("Password length less than 8", nameof(passwordHash));
            }
            Login = login;
            PasswordHash = passwordHash;
            Email = email;
            CreatedDate = createdDate;
            Roles = roles;
            IsActive = isActive;
        }

        private ApplicationUser() { }

        public void UpdateLogin(string login, DateTime updatedDate)
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

            SetUpdatedDate(updatedDate);
        }

        public void UpdateEmail(string email, DateTime updatedDate)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is empty", nameof(email));
            }
            if (email.Length > 50)
            {
                throw new ArgumentException("Email length more than 50", nameof(email));
            }
            if (email.Length < 10)
            {
                throw new ArgumentException("Email length less than 3", nameof(email));
            }

            Email = email;

            SetUpdatedDate(updatedDate);
        }

        public void UpdatePassword(string password, DateTime updatedDate)
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

            SetUpdatedDate(updatedDate);
        }

        public void UpdateIsActive(bool isActive) 
        {
            IsActive = isActive;
        }

        private void SetUpdatedDate(DateTime updatedDate)
        {
            if (updatedDate < UpdatedDate)
            {
                throw new ArgumentException("Incorrect update date", nameof(updatedDate));
            }

            UpdatedDate = updatedDate;
        }
    }
}
