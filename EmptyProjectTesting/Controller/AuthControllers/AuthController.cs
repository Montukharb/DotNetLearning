using EmptyProjectTesting.DbContexts;
using EmptyProjectTesting.DTO.AuthDto;
using EmptyProjectTesting.Entites.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                    var refreshToken = GenerateRefreshToken();

                    user.RefreshToken = refreshToken.Token;
                    user.TokenCreated = refreshToken.Created;
                    user.TokenExpires = refreshToken.Expires;

                    await _contextAuth.SaveChangesAsync();
                    SetRefreshTokenInCookie(refreshToken.Token);
                    return Ok(new { Token = token });

                }
                //else if (result == PasswordVerificationResult.SuccessRehashNeeded)
                //{
                //    user.PasswordHash = passwordhasher.HashPassword(user, loginDto.Password);

                //    await _contextAuth.SaveChangesAsync();

                //    var token = GenerateToken(user);
                //    var refreshToken = GenerateRefreshToken();

                //    return Ok(token);
                //}
                else
                {
                    //failed to login account 5 times
                    return Unauthorized();
                }

            }
            return Unauthorized();
        }


        [NonAction] //Yeh line batati hai ki yeh koi API endpoint nahi hai, sirf ek internal function hai
        private RefreshTokenDto GenerateRefreshToken()
        {
            //var randomNumber = new byte[32];
            //using var rng = RandomNumberGenerator.Create();
            //rng.GetBytes(randomNumber);
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            var tokenString = Convert.ToBase64String(randomBytes);

            var refreshToken = new RefreshTokenDto
            {
                Token = tokenString,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(7),
            };

            return refreshToken;
        }


        [NonAction] //Yeh line batati hai ki yeh koi API endpoint nahi hai, sirf ek internal function hai
        private string GenerateToken(User user)
        {
            //jwt token stored this type information in claims  
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,"user"), //By default role is user only Admin can change this
                //new Claim("Country","India")
                new Claim("Country",user.)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));  //! means not null get key from appsettings.json in bytes

            //SigningCredentials is used to Digital sign the token
            //HMACSHA256  Hash Based Message Authentication Code , Sha Security hash algorithm 
            //var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512); //little bit slower.

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

        [Authorize(Roles = "admin,user")] //admin,user this is or condition
        [Authorize(Policy = "SpecialPolicy", Roles = "admin")] //And condition but already policy bana rahe ho, to Role ko bhi policy ke andar hi rakhna better hota hai.
        //Multiple authorize attribue and condition
        [Authorize(Policy = "AdminOrIndiaP")] //custom policy use
        [HttpGet("profile/")]
        //[AllowAnonymous] authentication or authorization both are by passed any endpoints
        public Task<IActionResult> Profile()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var Name = User.FindFirst(ClaimTypes.Name)?.Value;
            var Email = User.FindFirst(ClaimTypes.Email)?.Value;
            //var country = User.FindFirst("country")?.Value; custom claim get
            return Task.FromResult<IActionResult>(Ok(new { id, Name, Email, Role = "null", Message = "Profile" }));
        }
        //FromResult is used to return 




        [HttpPost("refresh-token/")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            // 1. Browser ki cookie se purana refresh token Find out
            var oldTokenFromCookie = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(oldTokenFromCookie))
            {
                return Unauthorized("No refresh token found.");
            }

            // 2. Finding in Database ki yeh token kis user ka hai
            var user = await _contextAuth.Users.FirstOrDefaultAsync(u => u.RefreshToken == oldTokenFromCookie);

            // 3. Validation Check: Kya user mila? Aur kya token expire toh nahi hua?
            if (user == null)
            {
                return Unauthorized("Invalid Refresh Token.");
            }

            //Token expire check
            if (user.TokenExpires < DateTime.UtcNow)
            {
                return Unauthorized("Token Expired. Please login again.");
            }

            // 4. AGAR SAB SAHI HAI -> Toh naya Access Token Generated
            string newAccessToken = GenerateToken(user);

            // 5. Security ke liye new Refresh Token bhi Generated (Token Rotation)
            var brandNewRefreshToken = GenerateRefreshToken();

            // 6. DB mein purane token ki jagah new token updated
            user.RefreshToken = brandNewRefreshToken.Token;
            user.TokenCreated = brandNewRefreshToken.Created;
            user.TokenExpires = brandNewRefreshToken.Expires;

            await _contextAuth.SaveChangesAsync();

            // 7. Set new cookie
            SetRefreshTokenInCookie(brandNewRefreshToken.Token);

            // 8. Send new Access Token
            return Ok(new { Token = newAccessToken });
        }

        [NonAction] //Yeh line batati hai ki yeh koi API endpoint nahi hai, sirf ek internal function hai
        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, //javascript isko touch nahi kar sakti security ka liya
                Secure = true, // https ka liya hai production me run hoga localhost par false hi rakhe
                SameSite = SameSiteMode.Strict, //csrf attack se protection 
                Expires = DateTime.UtcNow.AddDays(7),

            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
            //Iska kya matlab hai? Jab aap Response.Cookies.Append karte hain, toh .NET response ke header mein ek Set - Cookie tag laga deta hai. Browser ise dekhte hi samajh jata hai ki mujhe is refreshToken ko apne andar chhupa kar rakhna hai.
        }

    }
}
