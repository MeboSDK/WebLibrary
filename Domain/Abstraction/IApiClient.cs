using Core.Response;

namespace Core.Abstraction;
public interface IApiClient
{
    Task<ApiResponse<TResponse>> GetDataAsync<TResponse>(string endpoint, string? jwtToken = null);
    Task<ApiResponse<TResponse>> PostDataAsync<TResponse>(object data,string endpoint, string? jwtToken = null);
}
