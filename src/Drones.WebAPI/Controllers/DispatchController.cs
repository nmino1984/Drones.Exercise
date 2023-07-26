using Drones.Application.Interfaces;
using Drones.Application.ViewModels.Drone.Request;
using Drones.Application.ViewModels.DroneMedication.Request;
using Microsoft.AspNetCore.Mvc;
using Drones.Utilities.Statics;

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

        /// <summary>
        /// This WebAPI belongs to DroneApplication, but in the Terms of the Exercise, I included it in the Dispatch Controller
        /// </summary>
        /// <param name="requestViewModel">The Drone to Register</param>
        /// <returns>The Response with the Result of the Action</returns>
        [HttpPost("RegisterDrone")]
        public async Task<IActionResult> RegisterDrone([FromBody] DroneRequestViewModel requestViewModel)
        {
            var response = await _dispatchApplication.RegisterDrone(requestViewModel);
            //I could put this line instead... We'll have the same Result
            //var response = await _droneApplication.RegisterDrone(requestViewModel); 
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

        /// <summary>
        /// This API Loads the List of Medications to the Drone. 
        /// </summary>
        /// <param name="input">A class that contains the DroneId and the List of Medications to Load</param>
        /// <returns>The Response with the Result of the Action</returns>
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

        /// <summary>
        /// This API Checks the List of Medications Loaded in a Drone Given
        /// </summary>
        /// <param name="droneId">Id of the Drone to Check The Load</param>
        /// <returns>The Response with the List of Medications of the Load</returns>
        [HttpGet("MedicationsByDrone/{droneId:int}")]
        public async Task<IActionResult> CheckingLoadedMedicationItemsByDroneGiven([FromRoute]int droneId)
        {
            var response = await _dispatchApplication.CheckingLoadedMedicationItemsByDroneGiven(droneId);

            return Ok(response);
        }

        /// <summary>
        /// This API Checks all the Available Drones For Loading. It means all in IDLE State
        /// </summary>
        /// <returns>The Response with the List of Available Drone Allowed to Load</returns>
        [HttpGet("AvailableDrones")]
        public async Task<IActionResult> CheckingAvailableDronesForLoading()
        {
            var response = await _dispatchApplication.CheckingAvailableDronesForLoaded();
            return Ok(response);
        }

        /// <summary>
        /// This API Check the battery level of a Given Drone
        /// </summary>
        /// <param name="droneId">Id of the Drone to Check The battery Level</param>
        /// <returns>The Response with the Battery level of the Given Drone</returns>
        [HttpGet("DroneBattery/{droneId:int}")]
        public async Task<IActionResult> CheckDroneBatteryLevelByDroneGiven([FromRoute] int droneId)
        {
            var response = await _dispatchApplication.CheckDroneBatteryLevelByDroneGiven(droneId);
            return Ok(response);
        }
    }
}
