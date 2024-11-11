using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LendACarAPI.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string TestEndpoint()
        {
            return "This is a test";
        }

    }
}
