using EmptyProjectTesting.DbContexts;
using EmptyProjectTesting.DTO.AuthDto;
using EmptyProjectTesting.Entites.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            var userExits = await _contextAuth.Users.FirstOrDefaultAsync(x => x.Email == Dto.Email);
            if (userExits is not null)
            {
                return BadRequest("User Already Exists");
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
                    var token = GenerateToken(user);
                    return Ok(token);

                }
                else if (result == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    user.PasswordHash = passwordhasher.HashPassword(user, loginDto.Password);

                    await _contextAuth.SaveChangesAsync();

                    var token = GenerateToken(user);

                    return Ok(token);
                }
                else
                {
                    //failed to login account 5 times
                    return Unauthorized();
                }

            }
            return Unauthorized();
        }

        private string GenerateToken(User user)
        {
            //jwt token stored this type information in claims  
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,"user") //By default role is user only Admin can change this
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));  //! means not null get key from appsettings.json in bytes

            //SigningCredentials is used to Digital sign the token
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //now create jwt token
            var token = new JwtSecurityToken(
                 issuer: _configuration["jwt:issuer"],
                 audience: _configuration["Jwt:audience"],
                 claims: claims,
                 expires: DateTime.UtcNow.AddHours(1),
                 signingCredentials: credentials
                );

            //Now send this token to the user/client

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Authorize]
        [HttpGet("profile/")]
        public Task<IActionResult> Profile()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var Name = User.FindFirst(ClaimTypes.Name)?.Value;
            var Email = User.FindFirst(ClaimTypes.Email)?.Value;

            return Task.FromResult<IActionResult>(Ok(new { id, Name, Email, Role = "null", Message = "Profile" }));
        }
    }
}
