using Core.Abstraction;
using MediatR;

namespace Application.Commands.UserC.Commands;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, string>
{
    private readonly IApiClient _apiClient;
    private readonly ITokenService _tokenService;

    public UserRegisterCommandHandler(IApiClient apiClient, ITokenService tokenService)
    {
        _apiClient = apiClient;
        _tokenService = tokenService;
    }
    public async Task<string> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await _apiClient.PostDataAsync<string>(request, "user/register");

        if (response.Success)
        {
            _tokenService.ProtectToken(response.Data);
            return response.Data;
        }
        else
            throw new ArgumentException(response.ErrorMessage);
    }
}
