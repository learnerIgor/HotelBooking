namespace UpdateUserPasswordByMq.Dtos
{
    public class UserDto
    {
        public string ApplicationUserId { get; init; } = default!;
        public string PasswordHash { get; init; } = default!;
    }
}
