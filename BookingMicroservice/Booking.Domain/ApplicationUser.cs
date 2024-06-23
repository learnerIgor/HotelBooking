namespace Booking.Domain
{
    public class ApplicationUser
    {
        public Guid ApplicationUserId { get; private set; }
        public string Login { get; private set; }
        public bool IsActive { get; private set; }
        public string Email { get; private set; }

        public IEnumerable<Reservation> Reservations { get; private set; }

        public ApplicationUser(Guid applicationUserId, string login, string email, bool isActive)
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
            ApplicationUserId = applicationUserId;
            Login = login;
            Email = email;
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

        public void UpdateEmail(string email)
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
        }

        private ApplicationUser() { }
    }
}
