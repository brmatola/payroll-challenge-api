using Microsoft.AspNetCore.Mvc;

namespace payroll_challenge_api.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "hello";
    }
}