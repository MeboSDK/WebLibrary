using Core.Abstraction;

namespace WebLibrary.Middlewares;

public class TokenUnprotectMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITokenService _tokenService;

    public TokenUnprotectMiddleware(RequestDelegate next, ITokenService tokenService)
    {
        _next = next;
        _tokenService = tokenService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Cookies.TryGetValue("authToken", out var protectedToken))
        {
            try
            {
                var unprotectedToken = _tokenService.UnprotectToken(protectedToken);
                context.Request.Headers["Authorization"] = $"Bearer {unprotectedToken}";
            }
            catch (Exception ex)
            {
                // Log the exception or handle token unprotection failure
            }
        }

        await _next(context);
    }
}
