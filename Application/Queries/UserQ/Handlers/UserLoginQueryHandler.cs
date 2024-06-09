using Application.Queries.UserQ.Queries;
using Core.Abstraction;
using MediatR;

namespace Application.Queries.UserQ.Handlers;

public class UserLoginQueryHandler : IRequestHandler<UserLogInQuery, string>
{
    private readonly IApiClient _apiClient;
    private readonly ITokenService _tokenService;

    public UserLoginQueryHandler(IApiClient apiClient, ITokenService tokenService)
    {
        _apiClient = apiClient;
        _tokenService = tokenService;
    }
    public async Task<string> Handle(UserLogInQuery request, CancellationToken cancellationToken)
    {
        var response = await _apiClient.PostDataAsync<string>(request, "user/login");

        if (response.Success) {
            var protectedToken = _tokenService.ProtectToken(response.Data);
            return protectedToken;
        }
        else
            throw new ArgumentException(response.ErrorMessage);
    }
}
