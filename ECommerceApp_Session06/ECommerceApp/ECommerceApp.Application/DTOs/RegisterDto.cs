namespace ECommerceApp.Application.DTOs
{
    public class RegisterDto
    {
        public string Email { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
