using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VeloNews.Models.UserViewModels;
using VeloNews.Services.IServices;

namespace VeloNews.Controllers.Api
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationApiController : ControllerBase
    {
        private IAuthenticationService _authenticationService;
        private IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthenticationApiController(IAuthenticationService authenticationService,
            IConfiguration configuration,
            IUserService userService)
        {
            _authenticationService = authenticationService;
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Incorrect data");
            }

            var user = _authenticationService.GetUserDataByNameAndPass(viewModel.Name, viewModel.Password);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var claims = new List<Claim>()
            {
                 new Claim("Id", user.Id.ToString()),
                 new Claim(ClaimTypes.Name, viewModel.Name)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(jwt);
        }

        [Route("GetLoginUsers")]
        public ActionResult GetLoginUsers()
        {
            var users = _userService.GetLoginUsers();

            if(users == null)
            {
                return NotFound("Not found");
            }

            return Ok(users);
        }
    }
}
