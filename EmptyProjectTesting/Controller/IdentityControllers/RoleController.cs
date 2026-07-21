using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmptyProjectTesting.Controller.IdentityControllers
{
    [ApiController]
    [Route("api/[controller)]")]
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
            var result = await _roleManager.CreateAsync(new IdentityRole(role));
            
            return Ok(result);
        }
    }
}
