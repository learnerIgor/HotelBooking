namespace Mail.Domain
{
    public class EmailHistory
    {
        public Guid EmailHistoryId { get; private set; }
        public Guid ApplicationUserId { get; private set; }
        public string Email {  get; private set; }
        public DateTime SendDate { get; private set; }

        public EmailHistory(Guid applicationUserId, string email, DateTime sendDate)
        {
            if(applicationUserId == Guid.Empty)
            {
                throw new ArgumentException("Incorrect format ApplicationUserId", nameof(applicationUserId));
            }
            if(string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email is null", nameof(email));
            }
            ApplicationUserId = applicationUserId;
            Email = email;
            SendDate = sendDate;
        }

        private EmailHistory() { }
    }
}
