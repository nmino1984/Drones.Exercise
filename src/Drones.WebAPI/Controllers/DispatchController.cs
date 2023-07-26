using Drones.Application.Interfaces;
using Drones.Application.ViewModels.Drone.Request;
using Drones.Application.ViewModels.DroneMedication.Request;
using Microsoft.AspNetCore.Mvc;
using Drones.Utilities.Statics;
using Hangfire;

namespace Drones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispatchController : ControllerBase
    {
        private readonly IDispatchApplication _dispatchApplication;
        private readonly IDroneApplication _droneApplication;

        public DispatchController(IDispatchApplication dispatchApplication, IDroneApplication droneApplication)
        {
            this._dispatchApplication = dispatchApplication;
            this._droneApplication = droneApplication;
        }

        [HttpPost("RegisterDrone")]
        public async Task<IActionResult> RegisterDrone([FromBody] DroneRequestViewModel requestViewModel)
        {
            var response = await _dispatchApplication.RegisterDrone(requestViewModel);
            return Ok(response);
        }

        //[HttpPost("LoadMedicationsToDrone")]
        //public async Task<IActionResult> LoadDroneWithMeditionItems(IFormFile uploadedFile, [FromBody] DispatchRequestViewModel input)
        //{
        //    var saveFilePath = Path.Combine("Images/", uploadedFile.FileName);
        //    using (var stream = new FileStream(saveFilePath, FileMode.Create))
        //    {
        //        await uploadedFile.CopyToAsync(stream);
        //    }

        //    var response = await _dispatchApplication.LoadDroneWithMeditionItems(input);

        //    return Ok(response);
        //}

        [HttpPost("LoadMedicationsToDrone")]
        public async Task<IActionResult> LoadDroneWithMeditionItems([FromBody] DispatchRequestViewModel input)
        {
            var response = await _dispatchApplication.LoadDroneWithMeditionItems(input);
            if (response.IsSuccess)
            {
                await _droneApplication.ChangeStateToDrone(input.droneId, StateTypes.LOADED);
            }
            return Ok(response);
        }

        [HttpGet("MedicationsByDrone/{droneId:int}")]
        public async Task<IActionResult> CheckingLoadedMedicationItemsByDroneGiven([FromRoute]int droneId)
        {
            var response = await _dispatchApplication.CheckingLoadedMedicationItemsByDroneGiven(droneId);

            return Ok(response);
        }

        [HttpGet("AvailableDrones")]
        public async Task<IActionResult> CheckingAvailableDronesForLoaded()
        {
            var response = await _dispatchApplication.CheckingAvailableDronesForLoaded();
            return Ok(response);
        }

        [HttpGet("DroneBattery/{droneId:int}")]
        public async Task<IActionResult> CheckDroneBatteryLevelByDroneGiven([FromRoute] int droneId)
        {
            var response = await _dispatchApplication.CheckDroneBatteryLevelByDroneGiven(droneId);
            return Ok(response);
        }
    }
}
