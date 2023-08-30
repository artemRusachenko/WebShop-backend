using lesson1_Simple_Functions___Controller.Exceptions;
using lesson1_Simple_Functions___Controller.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace lesson1_Simple_Functions___Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService userService;

        public AuthController(IAuthService userService)
        {
            this.userService = userService;
        }

        [HttpPost("signup")]

        public async Task<ActionResult<GetTokenDto>> SignUp(SignUpUserDto user)
        {
            try
            {
                var result = await userService.SignUp(user);
                return Ok(result);
            }
            catch(UserException ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("signin")]

        public async Task<ActionResult<GetTokenDto>> SignIn(SignInUserDto user)
        {
            try
            {
                var result = await userService.SignIn(user);
                return Ok(result);
            }
            catch(UserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                if(ex.Message == "User not found")
                    return NotFound(ex.Message);

                return BadRequest(ex.Message);
            }
            
        }
    }
}
