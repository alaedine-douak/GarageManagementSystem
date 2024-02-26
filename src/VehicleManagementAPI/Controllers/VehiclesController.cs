using Microsoft.AspNetCore.Mvc;
using VehicleManagementAPI.Model;
using Microsoft.EntityFrameworkCore;
using VehicleManagementAPI.DataAccess;

namespace VehicleManagementAPI.Controllers;

[Route("/api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly VehicleManagementDbContext _ctx;

    public VehiclesController(VehicleManagementDbContext ctx)
    {
        _ctx = ctx;
    }

    [HttpGet(Name = "GetAllVehicles")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _ctx.Vehicles.ToListAsync());
    }

    [HttpGet]
    [Route("{licenseNumber}", Name = "GetByLicenseNumber")]
    public async Task<IActionResult> GetByLicenseNumber(string licenseNumber)
    {
        var vehicle = await _ctx.Vehicles
            .FirstOrDefaultAsync(x => x.LicenseNumber == licenseNumber);

        if (vehicle is null) return NotFound();

        return Ok(vehicle);
    }

    [HttpPost(Name = "RegisterVehicle")]
    public async Task<IActionResult> Register([FromBody]Vehicle model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                Vehicle vehicle = new()
                {
                    LicenseNumber = model.LicenseNumber,
                    Brand = model.Brand,
                    Type = model.Type
                };

                _ctx.Vehicles.Add(vehicle);
                await _ctx.SaveChangesAsync();

                return CreatedAtRoute(
                    nameof(GetByLicenseNumber),
                    new { licenseNumber = vehicle.LicenseNumber },
                    vehicle);
            }

            return BadRequest();
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError("", "Unable to save changes");
            return StatusCode(StatusCodes.Status500InternalServerError);
            throw;
        }
    }
}