using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lesson1_Simple_Functions___Controller.Models;
using lesson1_Simple_Functions___Controller.Services.UserService;
using Microsoft.AspNetCore.Authorization;

namespace lesson1_Simple_Functions___Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {       
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /*[HttpGet]

        public async Task<ActionResult<List<GetUserDto>>> GetUsers()
        {
            var result = await userService.GetUsers();
            return Ok(result);
        }*/

        [HttpGet(), Authorize]

        public async Task<ActionResult<GetUserDto>> GetUserInfo()
        {
            var result = await userService.GetUserInfo();
            return Ok(result);
        }

        [HttpPut("data")]

        public async Task<ActionResult<GetUserDto?>> UpdateUserData(UpdateUserDataDto redactedUser)
        {
            var result = await userService.UpdateUserData(redactedUser);
            if (result == null)
            {
                return NotFound("User doesn't exist");
            }
            return Ok(result);
        }

        [HttpPut("password")]

        public async Task<ActionResult<GetUserDto?>> UpdateUserPassword(UpdateUserPasswordDto redactedUser)
        {
            var result = await userService.UpdateUserPassword(redactedUser);
            if (result == null)
            {
                return NotFound("User doesn't exist");
            }
            return Ok(result);
        }
    }
}
