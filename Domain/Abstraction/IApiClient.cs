namespace Core.Abstraction;

public interface IApiClient
{
    Task<string> GetDataAsync(string endpoint, string jwtToken);
    Task<string> PostDataAsync(object data,string endpoint, string jwtToken);
}
