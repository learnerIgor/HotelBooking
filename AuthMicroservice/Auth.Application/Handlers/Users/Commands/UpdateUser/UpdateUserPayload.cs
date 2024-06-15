namespace Auth.Application.Handlers.Users.Commands.UpdateUser
{
    public class UpdateUserPayload
    {
        public required string Login { get; init; } = default!;
    }
}