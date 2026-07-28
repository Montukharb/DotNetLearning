using EmptyProjectTesting.DTO.AuthDto;
using EmptyProjectTesting.IDENTITY;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace EmptyProjectTesting.Controller.IdentityControllers
{
    [ApiController]
    [Route("api/sign-in/[controller]")]
    public class SigninController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public SigninController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;

        }
        /*
        lockout = if you enter wrong password 3 or n(define any number acc to you) times then your account will be locked for some time.
        example = max failed attempts = 3
        lockout duration = 10 minutes.

        In Database table AspNetUsers there are three columns for LockoutEnabled, LockoutEnd and AccessFailedCount
        */
        //Mannual handle user login authentication

        [HttpPost("login-mannual/")]
        public async Task<IActionResult> LoginMannual([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email); //find user using email
            if (user is null)
            {
                return NotFound(new { Message = "User Not Found", loginDto.Email });
            }
            if (user.LockoutEnabled && user.LockoutEnd > DateTime.UtcNow) //check user possible to lock or not and check user locked or not
            {
                return BadRequest(new { Message = "Account Locked" });
            }
            //user.LockoutEnabled = false //set false to unlock unlimited try;
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);  //check and verify password
            if (!result)
            {
                user.AccessFailedCount++;

                if (user.AccessFailedCount >= 5)
                {
                    user.LockoutEnd = DateTime.UtcNow.AddMinutes(10);
                    user.AccessFailedCount = 0; // reset after lock
                    await _userManager.UpdateAsync(user);
                }

                var updateResult = await _userManager.UpdateAsync(user);

                if (!updateResult.Succeeded)
                {
                    return StatusCode(500, "Failed to update user.");
                }

                return Unauthorized(new { Message = "Invalid Credentials" });
            }
            //JWT and Refresh Token Generate Here
            user.AccessFailedCount = 0;
            await _userManager.UpdateAsync(user);
            return Ok(new { Message = "Login Successfully" });
        }


        //Automatic handle user login authentication
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            // 1. User Find
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return Unauthorized("Invalid Email");

            // 2. Check Lockout
            bool isLocked = await _userManager.IsLockedOutAsync(user);

            if (isLocked)
            {
                return BadRequest("Account Locked");
            }

            // 3. Check Password
            bool passwordValid =
                await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!passwordValid)
            {
                // 4. Increase Failed Count
                var failedResult = await _userManager.AccessFailedAsync(user);

                if (!failedResult.Succeeded)
                {
                    return StatusCode(500, failedResult.Errors);
                }

                // 5. Check Again
                if (await _userManager.IsLockedOutAsync(user))
                {
                    return BadRequest("Account Locked");
                }

                // Optional: Current Failed Count
                int count = await _userManager.GetAccessFailedCountAsync(user);

                return BadRequest($"Invalid Password. Failed Attempts : {count}");
            }

            // Password Correct

            // 6. Reset Failed Count
            var resetResult =  await _userManager.ResetAccessFailedCountAsync(user);

            if (!resetResult.Succeeded)
            {
                return StatusCode(500, resetResult.Errors);
            }

            // 7. Generate JWT

            return Ok("JWT Token Here");
        }

        /*
          Identity ki Methods

  Ab dekho Identity ne ye sab kaam methods me de diya.

  1. AccessFailedAsync()

  Tum ye likhte ho

  await _userManager.AccessFailedAsync(user);

  Internally approximately ye karta hai

  user.AccessFailedCount++;

  await _userManager.UpdateAsync(user);

  Agar configured limit cross ho jaye, Identity lockout bhi apply kar sakti hai.

  Return

  IdentityResult

  Ye batata hai operation database me successfully save hua ya nahi.

  2. ResetAccessFailedCountAsync()

  Tum ye likhte ho

  await _userManager.ResetAccessFailedCountAsync(user);

  Internally

  user.AccessFailedCount = 0;

  await _userManager.UpdateAsync(user);

  Return

  IdentityResult
  3. IsLockedOutAsync()

  Tum ye likhte ho

  bool locked =
  await _userManager.IsLockedOutAsync(user);

  Internally approximately ye check karta hai

  if(user.LockoutEnabled &&
     user.LockoutEnd > DateTimeOffset.UtcNow)
  {
      return true;
  }

  return false;

  Return

  bool
  4. SetLockoutEndDateAsync()

  Ye manually lock lagata hai.

  await _userManager.SetLockoutEndDateAsync(
      user,
      DateTimeOffset.UtcNow.AddMinutes(15));

  Database

  LockoutEnd

  =

  Now+15 Minutes

  Return

  IdentityResult
  5. GetAccessFailedCountAsync()
  int count =
  await _userManager.GetAccessFailedCountAsync(user);

  Return

  Current Failed Count

  Example

  4
        */
    }
}


/*
 JWT Web API me tumhare paas 2 options hote hain.

Option 1 (Most Common)

Sirf UserManager use karo.

var user = await _userManager.FindByEmailAsync(loginDto.Email);

if (user == null)
    return Unauthorized();

var valid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

if (!valid)
    return Unauthorized();

// JWT Generate

Isme lockout automatically handle nahi hota.

Tumhe manually karna padega.

await _userManager.AccessFailedAsync(user);

if (await _userManager.IsLockedOutAsync(user))
{
    return BadRequest("Account Locked");
}

await _userManager.ResetAccessFailedCountAsync(user);
 */