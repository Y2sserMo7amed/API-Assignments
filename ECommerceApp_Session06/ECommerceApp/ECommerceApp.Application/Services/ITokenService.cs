namespace ECommerceApp.Application.Services
{
   
    public interface ITokenService
    {
        string GenerateToken(string email, string displayName);
    }
}
