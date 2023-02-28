using jwt_auth.Data;
using jwt_auth.Model;
using jwt_auth.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace jwt_auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly UserContext _userContext;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<IdentityUser> userManager, UserContext userContext, ITokenService tokenService)
        {
            this._userManager = userManager;
            this._userContext = userContext;
            this._tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationRequest request)
        { 
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userManager.CreateAsync(new IdentityUser {  
                UserName = request.UserName,
                Email = request.Email,
            }, request.Password);

            if (result.Succeeded) 
            {
                request.Password = String.Empty;
                return CreatedAtAction(nameof(Register), new { email = request.Email});
            }
            result.Errors.ToList().ForEach(error => { ModelState.AddModelError(error.Code, error.Description); });
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null)
                return BadRequest("Invalid Credentials");

            var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isValidPassword)
                return BadRequest("Invalid Credentials");

            var userInDb = _userContext.Users.FirstOrDefault(u => u.Email == request.Email);
            if (userInDb == null)
                return Unauthorized();

            var jwtToken = _tokenService.CreateToken(userInDb);
            await _userContext.SaveChangesAsync();

            return Ok(new AuthResponse
            {
                Username = userInDb.UserName,
                Email = userInDb.Email,
                Token = jwtToken,
            });
        }
    }
}
