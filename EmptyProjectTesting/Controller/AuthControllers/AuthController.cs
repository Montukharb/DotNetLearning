using EmptyProjectTesting.DbContexts;
using EmptyProjectTesting.DTO.AuthDto;
using EmptyProjectTesting.Entites.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmptyProjectTesting.Controller.AuthControllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly AppDbContextAuth _contextAuth;
        public AuthController(IConfiguration configuration, ILogger<AuthController> logger, AppDbContextAuth contextAuth)
        {
            _configuration = configuration;
            _logger = logger;
            _contextAuth = contextAuth;
        }

        [HttpPost("register/")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto Dto)
        {
            if (Dto is null)
            {
                return BadRequest("should not be null");
            }
            var user = new User
            {
                Name = Dto.Name,
                Email = Dto.Email,
                PasswordHash = Dto.Password

            };
            var hashPass = new PasswordHasher<User>();
            //string _hashPassword = hashPass.HashPassword(user, password: Dto.Password); //both are same
            string _hashPassword = hashPass.HashPassword(user, password: user.PasswordHash);
            user.PasswordHash = _hashPassword;

            if (user.Name is not null && user.Email is not null && user.PasswordHash is not null)
            {
                _contextAuth.Users.Add(user);
                var effectRow = await _contextAuth.SaveChangesAsync();

                return effectRow > 0 ? Ok("User Created") : BadRequest("User Not Created");
            }
            else
            {
                return BadRequest("Enter complete data");
            }
        }

        [HttpPost("login/")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Enter Complete credentials");
            }
            //string password = loginDto.Password;
            //if (password is null)
            //{
            //    return BadRequest("Password should not be null");
            //}
            User? user = await _contextAuth.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
            if (user is not null)
            {
                var passwordhasher = new PasswordHasher<User>();
                var result = passwordhasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    Console.WriteLine("Login Success");
                    return Ok(user);

                }
                else if (result == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    var newUser = new { user, message = "Password needs to be more secure" };
                    Console.WriteLine("Login Success");

                    return Ok(newUser);
                }
                else
                {
                    return Unauthorized();
                }

            }
            return Unauthorized();
        }


    }
}
