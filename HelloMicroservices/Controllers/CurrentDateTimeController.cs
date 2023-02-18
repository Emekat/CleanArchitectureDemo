using Microsoft.AspNetCore.Mvc;

namespace HelloMicroservices{

[ApiController]
[Route("[controller]")]
    public class CurrentDateTimeController : ControllerBase
    {
        [HttpGet("/")]
        public object Get() => DateTime.UtcNow;
    }
}