using Drones.Application.Interfaces;
using Drones.Application.ViewModels.Request;
using Microsoft.AspNetCore.Mvc;

namespace Drones.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationController : ControllerBase
    {
        private readonly IMedicationApplication _medicationApplication;

        public MedicationController(IMedicationApplication medicationApplication)
        {
            this._medicationApplication = medicationApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListMedications()
        {
            var response = await _medicationApplication.ListMedications();
            return Ok(response);
        }

        [HttpGet("{medicationId:int}")]
        public async Task<IActionResult> MedicationById(int medicationId)
        {
            var response = await _medicationApplication.GetMedicationById(medicationId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterMedication([FromBody] MedicationRequestViewModel requestViewModel)
        {
            var response = await _medicationApplication.RegisterMedication(requestViewModel);
            return Ok(response);
        }

        [HttpPut("Edit/{medicationId:int}")]
        public async Task<IActionResult> Editmedication([FromRoute] int medicationId, [FromBody] MedicationRequestViewModel requestViewModel)
        {
            var response = await _medicationApplication.EditMedication(medicationId, requestViewModel);
            return Ok(response);
        }

        [HttpDelete("Delete/{medicationId:int}")]
        public async Task<IActionResult> DeleteDrone([FromRoute] int medicationId)
        {
            var response = await _medicationApplication.DeleteMedication(medicationId);
            return Ok(response);
        }
    }
}
