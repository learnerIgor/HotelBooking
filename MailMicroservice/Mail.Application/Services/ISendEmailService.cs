namespace Mail.Application.Services
{
    public interface ISendEmailService
    {
        Task<bool> SendEmailMessage(string checkIn, string checkOut, string login, string email, string hotel, string roomType, CancellationToken cancellationToken);
    }
}
