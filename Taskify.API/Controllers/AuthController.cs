using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Taskify.API.Interfaces;
using Taskify.API.Models;
using Taskify.API.Models.DTO;

namespace Taskify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("fixedwindow")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _signInManager;
        private readonly IConfiguration _config;

        public AuthController(ITokenService tokenService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> signInManager, IConfiguration config)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpPost]
        [Route("login")]
        [SwaggerOperation(Summary = "Fazer login de usuário.")]
        public async Task<IActionResult> Login([FromBody] LoginModelDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Username!);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password!))
            {
                var roles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = _tokenService.GenerateAccessToken(authClaims, _config);

                await _userManager.UpdateAsync(user);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        [SwaggerOperation(Summary = "Cadastrar um novo usuário.",
            Description = "Registra um novo usuário no sistema. A senha deve conter pelo menos uma letra maiúscula e um símbolo especial.")]
        public async Task<IActionResult> Register([FromBody] RegisterModelDTO modelDTO)
        {
            var userExists = await _userManager.FindByNameAsync(modelDTO.Username!);

            if (userExists != null)
                return BadRequest("User already exists!");

            ApplicationUser user = new()
            {
                Email = modelDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = modelDTO.Username
            };

            var result = await _userManager.CreateAsync(user, modelDTO.Password);

            if (!result.Succeeded)
                return BadRequest("Houve um erro ao realizar seu registro, tente uma senha mais forte \n" + result.Errors);

            return Ok(new { Username = user.UserName, Email = user.Email });
        }
    }
}
