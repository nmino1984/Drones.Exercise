using Drones.Application.Interfaces;
using Drones.Application.ViewModels.Drone.Request;
using Drones.Utilities.Statics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;

namespace Drones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DroneController : ControllerBase
    {
        private readonly IDroneApplication _droneApplication;

        public DroneController(IDroneApplication droneApplication)
        {
            this._droneApplication = droneApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListDrones()
        {
            var response = await _droneApplication.ListDrones();
            return Ok(response);
        }

        [HttpGet("{droneId:int}")]
        public async Task<IActionResult> DroneById(int droneId)
        {
            var response = await _droneApplication.GetDroneById(droneId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterDrone([FromBody] DroneRequestViewModel requestViewModel)
        {
            var response = await _droneApplication.RegisterDrone(requestViewModel);
            return Ok(response);
        }

        [HttpPut("Edit/{droneId:int}")]
        public async Task<IActionResult> EditDrone([FromRoute] int droneId, [FromBody] DroneRequestViewModel requestViewModel)
        {
            var response = await _droneApplication.EditDrone(droneId, requestViewModel);
            return Ok(response);
        }

        [HttpDelete("Delete/{droneId:int}")]
        public async Task<IActionResult> DeleteDrone([FromRoute] int droneId)
        {
            var response = await _droneApplication.DeleteDrone(droneId);
            return Ok(response);
        }

        [HttpPut("SetBatterylevel/{droneId:int}/{droneBatteryLevel:double}")]
        public async Task<IActionResult> SetBatteryLevel([FromRoute] int droneId, [FromRoute] double droneBatteryLevel)
        {
            var response = await _droneApplication.ChangeBatteryLevelToDrone(droneId, droneBatteryLevel);
            return Ok(response);
        }

        [HttpPut("SetDroneState/{droneId:int}/{droneState:int}")]
        public async Task<IActionResult> SetDroneStateDrone(int droneId, int droneState)
        {
            var response = await _droneApplication.ChangeStateToDrone(droneId, (StateTypes)droneState);
            return Ok(response);
        }
    }
}
