using Core.Abstraction;
using Infrastructure.ApiClients;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Services.Token;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataProtection();
        services.AddHttpClient<IApiClient, ApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["API:LibraryAPI"]);
        });

        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
