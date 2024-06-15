using MediatR;

namespace Booking.Application.Abstractions
{
    public interface IMqEmailService
    {
        Task SendEmailMessage(string queue, string message);
    }
}
