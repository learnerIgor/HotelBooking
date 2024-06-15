namespace Auth.Domain
{
    public class RefreshToken
    {
        public Guid RefreshTokenId { get; private set; }

        public Guid ApplicationUserId { get; private set; }
        public ApplicationUser ApplicationUser { get; private set; }

        public DateTime Expired { get; private set; }

        public RefreshToken(Guid applicationUserId, DateTime expired)
        {
            if(applicationUserId == Guid.Empty)
            {
                throw new ArgumentNullException("ApplicationUserId cannot be empty", nameof(applicationUserId));
            }
            if(expired < DateTime.UtcNow)
            {
                throw new ArgumentNullException("Expired cannot be in the past", nameof(expired));
            }
            ApplicationUserId = applicationUserId;
            Expired = expired;
        }

        public void UpdateExpired(DateTime expired)
        {
            if (expired < DateTime.UtcNow)
            {
                throw new ArgumentNullException("Expired cannot be in the past", nameof(expired));
            }
            Expired = expired;
        }

        private RefreshToken() { }
    }
}
