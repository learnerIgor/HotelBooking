using MediatR;
using Mail.Domain;
using Mail.Application.Abstractions.Persistence.Repositories.Write;
using Microsoft.Extensions.Logging;
using Mail.Application.Services;


namespace Mail.Application.Handlers.Commands.SendEmail
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, bool>
    {
        private readonly IBaseWriteRepository<EmailHistory> _emailHistory;
        private readonly ILogger<SendEmailCommandHandler> _logger;
        private readonly ISendEmailService _sendEmailService;

        public SendEmailCommandHandler(
            IBaseWriteRepository<EmailHistory> emailHistory,
            ILogger<SendEmailCommandHandler> logger,
            ISendEmailService sendEmailService)
        {
            _emailHistory = emailHistory;
            _logger = logger;
            _sendEmailService = sendEmailService;
        }

        public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var mail = await _sendEmailService.SendEmailMessage(request.CheckIn, request.CheckOut, request.Login, request.Email, request.Hotel, request.RoomType, cancellationToken);
            var idGuidUser = Guid.Parse(request.ApplicationUserId);
            var emaiHistory = new EmailHistory(idGuidUser, request.Email, DateTime.Now);
            await _emailHistory.AddAsync(emaiHistory, cancellationToken);
            _logger.LogInformation($"Sended email to {request.Login}");

            return mail;
        }
    }
}
