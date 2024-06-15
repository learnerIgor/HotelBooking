using Grpc.Core;
using GrpcGreeter;
using MediatR;
using Users.Application.Handlers.Queries.GetUserByLogin;

namespace Users.Api.gRPC
{
    public class GRPCUsersService : UsersService.UsersServiceBase
    {
        private readonly IMediator _mediator;
        public GRPCUsersService(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Get user
        /// </summary>
        /// <returns></returns>
        public override async Task<UserReply> GetUser(GetUserRequest request, ServerCallContext context)
        {
            var query = new GetUserByLoginQuery
            {
                FreeText = request.LoginUser
            };
            var dto = await _mediator.Send(query, context.CancellationToken);
            var replay = new UserReply
            {
                ApplicationUserId = dto.ApplicationUserId.ToString(),
                Login = dto.Login,
                IsActive = dto.IsActive,
                Password = dto.PasswordHash
            };

            return replay;
        }
    }
}
