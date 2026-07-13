using ECommerceApp.Application.DTOs;
using ECommerceApp.Application.Services;
using ECommerceApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceApp.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (await _userManager.FindByEmailAsync(registerDto.Email) != null)
                return BadRequest("This email is already in use.");

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email   
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(e => e.Description));

            return Ok(new UserDto
            {
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Token = _tokenService.GenerateToken(user.Email!, user.DisplayName)
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return Unauthorized("Invalid email or password.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Invalid email or password.");

            return Ok(new UserDto
            {
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Token = _tokenService.GenerateToken(user.Email!, user.DisplayName)
            });
        }

        [HttpGet("emailexists")]
        public async Task<IActionResult> CheckEmailExists([FromQuery] string email)
        {
            var exists = await _userManager.FindByEmailAsync(email) != null;
            return Ok(exists);
        }

        
        [Authorize]
        [HttpGet("currentuser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            if (email == null)
                return Unauthorized();

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return NotFound("User not found.");

            return Ok(new UserDto
            {
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Token = _tokenService.GenerateToken(user.Email!, user.DisplayName)
            });
        }
    }
}
