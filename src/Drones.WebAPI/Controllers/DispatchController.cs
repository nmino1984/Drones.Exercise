using Drones.Application.Interfaces;
using Drones.Application.ViewModels.Drone.Request;
using Drones.Application.ViewModels.DroneMedication.Request;
using Drones.Utilities.Statics;
using Microsoft.AspNetCore.Mvc;

namespace Drones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispatchController : ControllerBase
    {
        private readonly IDroneMedicationApplication _droneMedicationApplication;

        public DispatchController(IDroneMedicationApplication droneMedicationApplication)
        {
            this._droneMedicationApplication = droneMedicationApplication;           

        }

        [HttpPost("RegisterDrone")]
        public async Task<IActionResult> RegisterDrone([FromBody] DroneRequestViewModel requestViewModel)
        {
            List<string> listaSerials = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                listaSerials.Add(SerialGenerate.Generate());
            }
            var response = await _droneMedicationApplication.RegisterDrone(requestViewModel);
            return Ok(response);
        }

        [HttpGet("LoadMedications")]
        public async Task<IActionResult> LoadDroneWithMeditionItems([FromBody] DispatchRequestViewModel input)
        {
            var response = await _droneMedicationApplication.LoadDroneWithMeditionItems(input);
            return Ok(response);
        }

        [HttpGet("MedicationsByDrone/{droneId:int}")]
        public async Task<IActionResult> CheckingLoadedMedicationItemsByDroneGiven([FromRoute]int droneId)
        {
            var response = await _droneMedicationApplication.CheckingLoadedMedicationItemsByDroneGiven(droneId);
            return Ok(response);
        }

        [HttpGet("AvailableDrones")]
        public async Task<IActionResult> CheckingAvailableDronesForLoaded([FromRoute] int droneId)
        {
            var response = await _droneMedicationApplication.CheckingAvailableDronesForLoaded();
            return Ok(response);
        }

        [HttpGet("DroneBattery/{droneId:int}")]
        public async Task<IActionResult> CheckDroneBatteryLevelByDroneGiven([FromRoute] int droneId)
        {
            var response = await _droneMedicationApplication.CheckDroneBatteryLevelByDroneGiven(droneId);
            return Ok(response);
        }
    }
}
