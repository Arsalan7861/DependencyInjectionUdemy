using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.WebApplication.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ValuesController(IBuilder builder) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var message = builder.BuilHouse();
            return Ok(message);
        }
    }
}
