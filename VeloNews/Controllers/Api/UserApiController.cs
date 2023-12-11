using Data.Interface.DataModels.UserDataModels;
using Microsoft.AspNetCore.Mvc;
using VeloNews.Services.IServices;

namespace VeloNews.Controllers.Api
{
    [ApiController]
    [Route("/api/users")]
    public class UserApiController : Controller
    {
        private IUserService _userService;
        private IAuthenticationService _authenticationService;

        public UserApiController(IUserService userService, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public ActionResult<UserApiData> GetAllUsers()
        {
            var usersData = _authenticationService.GetAllUsersForApi();

            if (usersData == null)
            {
                return NotFound();
            }

            return Ok(usersData);
        }

        [HttpGet("{id}")]
        public ActionResult<UserApiData> GetUser(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var data = _authenticationService.GetUserForApi(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }
    }
}
