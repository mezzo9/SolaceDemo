
using Microsoft.AspNetCore.Mvc;

namespace ThermostatAPI;

[ApiController]
[Route("[controller]")]
public class Thermostat : ControllerBase
{
    // GET
    public IActionResult Get()
    {
        return Ok(new { result = Thermostats.AllThermostats });
    }
}