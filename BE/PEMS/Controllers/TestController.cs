using Microsoft.AspNetCore.Mvc;
using PEMS.Application;
using PEMS.Domain;

namespace PEMS.Controllers
{
    /// <summary>
    /// Controller test
    /// </summary>
    /// CreatedBy: TTANH (02/06/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController()
        {
        }

        [HttpGet()]
        public IActionResult Get()
        {
            return StatusCode(StatusCodes.Status200OK, $"Ttanh6 server build at: {DateTime.Now}");
        }
    }
}
