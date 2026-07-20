using EmptyProjectTesting.DTO.IdentityDTO;
using EmptyProjectTesting.IDENTITY;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmptyProjectTesting.Controller.IdentityControllers
{
    [ApiController]
    [Route("api/identity/auth/[controller]")]
    public class AccountController : ControllerBase
    {
        //user manager is a high level service that is used to manage the users in the database and perform operations on them such as creating, updating, deleting, and retrieving users, passwordVerify, roleAssign, ClaimManaged etc.
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<ApplicationUser> userManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _logger = logger;

        }

        //Register User
        [HttpPost("register/")]
        public async Task<IActionResult> RegisterUser([FromBody] IdentityCreateUserDto userDto)
        {
            var user = new ApplicationUser
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName ?? "Not Provided",
                UserName = userDto.UserName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber
                //passwordhash is a property that is used to store the hashed password of the user isko createasync me pass karte hai
            };
            var result = await _userManager.CreateAsync(user, userDto.Password); //automatic password converts to hashed password.

            if (!result.Succeeded)
            {
                _logger.LogError("User Registration Failed {Registration Error}", result.Errors);
                return BadRequest(result.Errors);
            }

            return Ok("User Registered Successfully");
        }
        /* 
         By Email
 var user = await _userManager.FindByEmailAsync("montu@gmail.com");
         By Username
 var user = await _userManager.FindByNameAsync("montu");
         By Id
 var user = await _userManager.FindByIdAsync(userId);
        
         */

        //find user by email id
        [HttpGet("finduser/{email}")]
        public async Task<IActionResult> FindUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return NotFound("User Not Found");
            }
            _logger.LogInformation("user found successfully");
            return Ok(new { user, Message = "User Found Successfully" });
        }

        //Update user
        [HttpPut("updateuser/{email}")]
        public async Task<IActionResult> UpdateUser(string email, [FromBody] IdentityUpdateDto updateDto)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return NotFound(new { Message = "User Not Found", email });
            }

            user.FirstName = updateDto.FirstName;
            user.LastName = updateDto.LastName;
            user.UserName = updateDto.UserName;
            user.Email = updateDto.Email;
            user.PhoneNumber = updateDto.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            return Ok(new { result, Message = "User Updated Successfully" });
        }

        [HttpPatch("updatepassword/{email}")]
        public async Task<IActionResult> UpdatePassword(string email, [FromBody] IdentityUpdatePasswordDto updatePasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return NotFound(new { Message = "User Not Found", email });
            }
            var result = await _userManager.ChangePasswordAsync(user, updatePasswordDto.OldPassword, updatePasswordDto.NewPassword);
            return Ok(new { result, Message = "Password Updated Successfully" });
        }

        //Delete user
        [HttpDelete("deleteuser/{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return NotFound(new { Message = "User Not Found", email });
            }
            var result = await _userManager.DeleteAsync(user);
            return Ok(new { result, Message = "User Deleted Successfully" });
        }

        //check password
        [HttpGet("checkpassword/{email}/{password}")]
        public async Task<IActionResult> CheckPassword(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return NotFound(new { Message = "User Not Found", email });
            }
            var result = await _userManager.CheckPasswordAsync(user, password);
            return Ok(new { result, Message = "Password Checked/Verified Successfully" });
        }

        //reset password used only during learning purpose actual forget password used email.
        [HttpPatch("resetpassword/{email}/{password}")]
        public async Task<IActionResult> ResetPassword(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return NotFound(new { Message = "User Not Found", email });
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, password);
            return Ok(new { result, Message = "Password Reset Successfully" });

            /*
             Forgot Password
                   │
                   ▼
            User enters Email
                   │
                   ▼
            GeneratePasswordResetTokenAsync()
                   │
                   ▼
            Create Reset Link
                   │
                   ▼
            Send Email
                   │
                   ▼
            User clicks Link
                   │
                   ▼
            Frontend opens Reset Password Page
                   │
                   ▼
            User enters New Password
                   │
                   ▼
            ResetPasswordAsync(user, token, newPassword)

             */
        }

        //email verify and confirmation token not use as a production
        [HttpGet("verifyemail/{email}")]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is not null)
            {

            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _userManager.ConfirmEmailAsync(user, token);

            return Ok("verifyemailsuccessful");
        }

        /*
         12. Roles

        Role Add

        await _userManager.AddToRoleAsync(user, "Admin");

        Remove

        await _userManager.RemoveFromRoleAsync(user, "Admin");

        Check

        bool isAdmin =
        await _userManager.IsInRoleAsync(user, "Admin");

        Get All

        var roles =
        await _userManager.GetRolesAsync(user);
        13. Claims

        Add

        await _userManager.AddClaimAsync(
            user,
            new Claim("country","India"));

        Remove

        await _userManager.RemoveClaimAsync(
            user,
            claim);

        Read

        var claims =
        await _userManager.GetClaimsAsync(user);
        14. Lock User
        await _userManager.SetLockoutEndDateAsync(
            user,
            DateTimeOffset.UtcNow.AddDays(1));
        15. Access Failed

        Wrong password count.

        await _userManager.AccessFailedAsync(user);

        Reset

        await _userManager.ResetAccessFailedCountAsync(user);
        16. Password Hash

        Hash

        string hash =
        _userManager.PasswordHasher.HashPassword(
            user,
            "Password@123");

        Verify

        _userManager.PasswordHasher.VerifyHashedPassword(...)
         */
    }
}
