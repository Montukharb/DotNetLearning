using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestingController : ControllerBase
    {

        public Task Get()
        {
            //fix ok
            return Task.CompletedTask;
        }
    }
}
