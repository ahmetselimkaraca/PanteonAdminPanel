using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PanteonAdminPanel.API.DTO.AuthDTO;
using PanteonAdminPanel.API.Repositories;

namespace PanteonAdminPanel.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Email
            };

            var result = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("User created");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByNameAsync(loginRequestDto.Username);

            if (user == null)
                return BadRequest();

            var result = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);


            if (!result)
                return BadRequest();

            var token = _tokenRepository.GenerateToken(user);

            return Ok(token);
        }

    }
}
