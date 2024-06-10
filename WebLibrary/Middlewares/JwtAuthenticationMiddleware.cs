using Core.Abstraction;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static System.Formats.Asn1.AsnWriter;

namespace WebLibrary.Middlewares;

public class JwtAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public JwtAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Cookies.TryGetValue("authToken", out var token))
        {
            using (var scope = context.RequestServices.CreateScope())
            {
                var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
                var unprotectedToken = tokenService.UnprotectToken(token);

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(unprotectedToken) as JwtSecurityToken;

                if (jwtToken != null)
                {
                    var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
                    context.User = new ClaimsPrincipal(identity);
                }
            }
        }

        await _next(context);
    }
}
