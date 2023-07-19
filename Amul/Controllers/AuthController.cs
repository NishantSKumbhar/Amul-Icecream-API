using Amul.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Amul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        // POST : api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestGetDTO registerRequestGetDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestGetDTO.Username,
                Email = registerRequestGetDTO.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestGetDTO.Password);

            if (identityResult.Succeeded)
            {
                // Add Roles to user
                if(registerRequestGetDTO.Roles != null && registerRequestGetDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestGetDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User Registered Sucessfully, Please Login.");
                    }
                }
            }

            return BadRequest("Something Went Wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestGetDTO loginRequestGetDTO)
        {
            var user = await userManager.FindByEmailAsync(loginRequestGetDTO.Username);
            if(user != null) 
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestGetDTO.Password);

                if (checkPasswordResult)
                {
                    // Create Token
                    return Ok();
                }
            }

            return BadRequest("Username or Password not Correct!.");
        }
    }
}
