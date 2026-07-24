using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmptyProjectTesting.Controller.IdentityControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(ILogger<RoleController> logger, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _roleManager = roleManager;
        }

        [HttpPost("create-role/{role:alpha}")]
        public async Task<IActionResult> CreateRole(string role)
        {
            //find role
            var resultRole = await _roleManager.FindByNameAsync(role);
            if (resultRole is not null)
            {
                _logger.LogInformation("resultRole {RoleFind}", resultRole); //possible ouput string 

                resultRole.Name = "updatedAdminRole"; //assigne new role value in old role
                await _roleManager.UpdateAsync(resultRole);  //update role

            }

            //read role
            bool roleExists = await _roleManager.RoleExistsAsync(role);

            if (roleExists)
            {
                return BadRequest(new { Message = "Role Already Exists" });
            }
            var result = await _roleManager.CreateAsync(new IdentityRole(role)); //create role

            return Ok(result); //return success
        }

        //Delete role
        [HttpDelete("delete-role/{role:alpha}")]
        public async Task<IActionResult> DeleteRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                return BadRequest(new { Message = "Empty role provided" });
            }

            var result = await _roleManager.FindByNameAsync(role);
            if (result is null)
            {
                return NotFound(new { Message = "Role Not Found" });
            }

            var resultRole = await _roleManager.DeleteAsync(result);
            if (!resultRole.Succeeded)
            {
                return BadRequest($"Failed to delete role {resultRole}");
            }
            return Ok(new { Message = "Role Deleted Successfully", resultRole });



        }
    }
}
