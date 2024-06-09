namespace Core.Abstraction
{
    public interface ITokenService
    {
        string Protect(string token);
        string Unprotect(string protectedToken);
    }
}
