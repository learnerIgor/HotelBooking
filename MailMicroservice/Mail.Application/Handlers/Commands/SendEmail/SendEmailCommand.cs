using MediatR;

namespace Mail.Application.Handlers.Commands.SendEmail
{
    public class SendEmailCommand : IRequest<bool>
    {
        public string ApplicationUserId { get; init; } = default!;
        public string Login { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string Hotel { get; init; } = default!;
        public string RoomType { get; init; } = default!;
        public string CheckIn { get; init; } = default!;
        public string CheckOut { get; init; } = default!;
    }
}
