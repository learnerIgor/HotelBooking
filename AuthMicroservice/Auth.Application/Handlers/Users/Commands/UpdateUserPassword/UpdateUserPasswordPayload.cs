namespace Auth.Application.Handlers.Users.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordPayload
    {
        public required string PasswordHash { get; init; } = default!;
    }
}