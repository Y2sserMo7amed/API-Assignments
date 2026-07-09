using ECommerceApp.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerceApp.API.Attributes
{
 
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _durationInSec;

      
        public RedisCacheAttribute(int durationInSec = 90)
        {
            _durationInSec = durationInSec;
        }

        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices
                .GetRequiredService<ICacheService>();

            
            var cacheKey = CreateCacheKey(context.HttpContext.Request);

            var cachedData = await cacheService.GetAsync(cacheKey);

            if (cachedData != null)
            {
              
                context.Result = new ContentResult
                {
                    Content = cachedData,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                return;
            }

            var executedContext = await next();

            if (executedContext.Result is OkObjectResult okResult && okResult.Value != null)
            {
                await cacheService.SetAsync(cacheKey, okResult.Value, _durationInSec);
            }
        }

       
        private static string CreateCacheKey(HttpRequest request)
        {
            var keyParts = new List<string> { request.Path };

            foreach (var (key, value) in request.Query.OrderBy(q => q.Key))
            {
                keyParts.Add($"{key}={value}");
            }

            return string.Join("|", keyParts).ToLower();
        }
    }
}
