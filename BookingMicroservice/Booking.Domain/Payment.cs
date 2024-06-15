namespace Booking.Domain
{
    public class Payment
    {
        public Guid PaymentId { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public decimal Amount { get; private set; }
        public bool IsSendEmail { get; private set; }
        public bool IsActive { get; private set; }

        public Reservation Reservation { get; private set; }


        public Payment(DateTime paymentDate, decimal amount, bool isSendEmail, bool isActive)
        {
            if (paymentDate < DateTime.MinValue)
            {
                throw new ArgumentNullException("PaymentDate cannot be in the past", nameof(paymentDate));
            }
            if (amount <= 0)
            {
                throw new ArgumentNullException("Amount cannot be less than 0", nameof(amount));
            }
            PaymentDate = paymentDate;
            Amount = amount;
            IsSendEmail = isSendEmail;
            IsActive = isActive;
        }

        public void UpdateIsSendEmail(bool isSendEmail)
        {
            IsSendEmail = isSendEmail;
        }

        public void UpdateIsActive(bool isActive)
        {
            IsActive = isActive;
        }

        private Payment() { }
    }
}
