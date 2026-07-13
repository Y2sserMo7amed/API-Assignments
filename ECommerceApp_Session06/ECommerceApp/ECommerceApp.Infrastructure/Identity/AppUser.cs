using Microsoft.AspNetCore.Identity;

namespace ECommerceApp.Infrastructure.Identity
{
    
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; } = default!;
    }
}
