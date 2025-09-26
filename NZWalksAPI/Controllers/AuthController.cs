using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.DTOs;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
    

        public AuthController(UserManager<IdentityUser>userManager , ITokenRepository tokenRepository )
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //Post api/auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var IdentityUser = new IdentityUser
            {
                UserName = registerRequestDto.username,
                Email = registerRequestDto.username
            };
            var identityResult = await userManager.CreateAsync(IdentityUser, registerRequestDto.password);

            if (identityResult.Succeeded)
            {
                if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRoleAsync(IdentityUser, registerRequestDto.Roles);
                }
                if (identityResult.Succeeded)
                {
                    return Ok("User Registered Successfully, You can now login");
                }
            }
            return BadRequest("Something went wrong");



        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        { 
            var user = await userManager.FindByEmailAsync(loginRequestDto.username); //find user by email
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.password);

                if (checkPasswordResult)
                {

                    // Generate Roles for the user
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {


                        //Generate JWT Token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };return Ok(response);
                    }    
                
                }
            }
            return BadRequest("Incorrect username or password");
        }

    }
}






