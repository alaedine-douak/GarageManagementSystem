using Microsoft.AspNetCore.Mvc;

namespace VehicleManagementAPI.Controllers;

[Route("/api/[controller]")]
public class VehiclesController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("vehicles");
    }
}