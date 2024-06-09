using Core.Abstraction;
using Microsoft.AspNetCore.DataProtection;

namespace Infrastructure.Services.Token;

public class TokenService : ITokenService
{
    private readonly IDataProtector _protector;

    public TokenService(IDataProtectionProvider dataProtectionProvider)
    {
        _protector = dataProtectionProvider.CreateProtector("jwtTokenProtection");
    }
    public string ProtectToken(string token)
    {
        return _protector.Protect(token);
    }

    public string UnprotectToken(string protectedToken)
    {
        return _protector.Unprotect(protectedToken);
    }
}
