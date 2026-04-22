using Drones.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Drones.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BatteryLogController : ControllerBase
{
    private readonly IBatteryLogApplication _batteryLogApplication;

    public BatteryLogController(IBatteryLogApplication batteryLogApplication)
    {
        _batteryLogApplication = batteryLogApplication;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _batteryLogApplication.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("drone/{droneId:int}")]
    public async Task<IActionResult> GetByDrone([FromRoute] int droneId)
    {
        var response = await _batteryLogApplication.GetByDroneIdAsync(droneId);
        return Ok(response);
    }
}
