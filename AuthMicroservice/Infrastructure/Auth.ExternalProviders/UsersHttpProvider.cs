using Auth.Application.Abstractions.ExternalProviders;
using Auth.Application.Abstractions.Persistence.Repositories.Read;
using Auth.Application.Abstractions.Persistence.Repositories.Write;
using Auth.ExternalProviders.Exceptions;
using Auth.Domain;
using Microsoft.Extensions.Configuration;
using System.Net;
using Auth.ExternalProviders.Models;
using System.Text.Json;

namespace Auth.ExternalProviders
{
    public class UsersHttpProvider : IUsersProvider
    {
        private readonly IBaseWriteRepository<ApplicationUser> _users;
        private readonly IBaseReadRepository<ApplicationUserRole> _userRoles;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public UsersHttpProvider(IHttpClientFactory httpClientFactory, IConfiguration configuration, IBaseWriteRepository<ApplicationUser> users, IBaseReadRepository<ApplicationUserRole> userRoles) 
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _users = users;
            _userRoles = userRoles;
        }  
        public async Task<ApplicationUser> GetUserAsync(string login, CancellationToken cancellationToken)
        {
            var result = await _users.AsAsyncRead().SingleOrDefaultAsync(u => u.Login == login, cancellationToken);
            if (result != null) 
            {
                return result;
            }

            var userServiceUrl = _configuration["UserServiceApiUrl"];
            var getUserApiMethodUrl = $"{userServiceUrl}/api/v1/Users/External/{login}";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, getUserApiMethodUrl);
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "UserService";
                var requestUrlMessage = $"request url '{getUserApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }

            var jsonResult = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
            var getUserDto = JsonSerializer.Deserialize<GetUserDto>(jsonResult, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var rolesDomains = (await _userRoles.AsAsyncRead().ToArrayAsync(cancellationToken)).Where(r => getUserDto!.Roles.Contains(r.ApplicationUserRoleId)).ToArray();
            result = new ApplicationUser(getUserDto!.ApplicationUserId, getUserDto.Login,getUserDto.PasswordHash, rolesDomains, getUserDto.IsActive);
            result = await _users.AddAsync(result, cancellationToken);
            return result;
        }
    }
}
