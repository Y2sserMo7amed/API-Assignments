namespace ECommerceApp.Application.Services
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string key);
        Task SetAsync(string key, object value, int durationInSeconds);
    }
}
