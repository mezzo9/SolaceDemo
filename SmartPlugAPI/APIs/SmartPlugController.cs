using Microsoft.AspNetCore.Mvc;

namespace SmartPlugAPI.APIs;

[ApiController]
[Route("[controller]")]
public class SmartPlugController  : ControllerBase
{
    public IActionResult Get()
    {
        return Ok(new { result = SmartPlugs.AllSmartPlugs });
    }
}