using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken != null)
            {
                var identity = new ClaimsIdentity(jwtToken.Claims, "jwt");
                context.User = new ClaimsPrincipal(identity);
            }
        }

        await _next(context);
    }
}
