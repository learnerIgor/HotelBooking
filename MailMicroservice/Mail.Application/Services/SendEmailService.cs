using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Mail.Application.Services
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IConfiguration _configuration;

        public SendEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailMessage(string checkIn, string checkOut, string login, string email, string hotel, string roomType, CancellationToken cancellationToken)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_configuration["MailSettings:From"], _configuration["MailSettings:EmailFrom"]));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = _configuration["MailSettings:Subject"];
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = $"\"Dear {login},\r\n\r\nWe are pleased to confirm that you have successfully booked a room at \"{hotel}\" for your upcoming stay. Your reservation details are as follows:\r\n\r\nCheck-in date: {checkIn[..10]}\r\n" +
                $"Check-out date: {checkOut[..10]}\r\nRoom Type: {roomType}\r\n\r\nPlease keep this information handy for any future reference.\r\n\r\nWe look forward to welcoming you to our " +
                $"hotel and ensuring you have a wonderful stay.\r\n\r\nBest regards,\r\nHotelBooking Team\""
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_configuration["MailSettings:Host"], int.Parse(_configuration["MailSettings:Port"]!), SecureSocketOptions.SslOnConnect, cancellationToken);
            await client.AuthenticateAsync(_configuration["MailSettings:EmailFrom"], _configuration["MailSettings:Password"], cancellationToken);
            await client.SendAsync(emailMessage, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);

            return true;
        }
    }
}
