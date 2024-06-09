namespace Core.Abstraction
{
    public interface ITokenService
    {
        string ProtectToken(string token);
        string UnprotectToken(string protectedToken);
    }
}
