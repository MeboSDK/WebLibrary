using Core.Abstraction;
using Infrastructure.ApiClients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.DataProtection;
using Infrastructure.Services.Token;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDataProtection();
        services.AddHttpClient<IApiClient, ApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:44391/");
        });

        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
