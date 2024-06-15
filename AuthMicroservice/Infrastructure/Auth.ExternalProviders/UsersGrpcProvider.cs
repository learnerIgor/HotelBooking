using Auth.Application.Abstractions.ExternalProviders;
using Auth.Domain;
using Auth.Application.Abstractions.Persistence.Repositories.Read;
using Auth.Application.Abstractions.Persistence.Repositories.Write;
using Auth.ExternalProviders.Exceptions;
using Microsoft.Extensions.Configuration;
using Grpc.Net.Client;
using GrpcGreeter;
using Auth.Domain.Enums;

namespace Auth.ExternalProviders
{
    public class UsersGrpcProvider : IUsersProvider
    {
        private readonly IBaseWriteRepository<ApplicationUser> _users;
        private readonly IBaseReadRepository<ApplicationUserRole> _userRoles;
        private readonly IConfiguration _configuration;

        public UsersGrpcProvider(
            IConfiguration configuration, 
            IBaseWriteRepository<ApplicationUser> users, 
            IBaseReadRepository<ApplicationUserRole> userRoles)
        {
            _configuration = configuration;
            _users = users;
            _userRoles = userRoles;
        }
        public async Task<ApplicationUser> GetUserAsync(string login, CancellationToken cancellationToken)
        {
            var result = await _users.AsAsyncRead().SingleOrDefaultAsync(u => u.Login == login & u.IsActive, cancellationToken);
            if (result != null)
            {
                return result;
            }

            var requestUrl = _configuration["UserGrpcServiceApiUrl"];
            var channel = GrpcChannel.ForAddress(requestUrl!);
            var client = new UsersService.UsersServiceClient(channel);

            try
            {
                var resultUser = client.GetUser(new GetUserRequest
                {
                    LoginUser = login,
                }, cancellationToken: cancellationToken);

                var userRoles = resultUser.Roles.ToArray();
                var rolesDomains = (await _userRoles.AsAsyncRead().ToArrayAsync(cancellationToken)).Where(r => r.ApplicationUserRoleId == (int)ApplicationUserRolesEnum.Client).ToArray();
                result = new ApplicationUser(new Guid(resultUser.ApplicationUserId), resultUser.Login, resultUser.Password, rolesDomains, resultUser.IsActive);
                return await _users.AddAsync(result, cancellationToken);
            }
            catch (Exception)
            {
                var serviceName = "UserService";
                throw new ExternalServiceNotAvailable(serviceName, requestUrl!);
            }
        }
    }
}
