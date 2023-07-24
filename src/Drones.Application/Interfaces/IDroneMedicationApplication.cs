using Drones.Application.Commons.Bases;
using Drones.Application.ViewModels.Drone.Request;
using Drones.Application.ViewModels.Drone.Response;
using Drones.Application.ViewModels.DroneMedication.Request;
using Drones.Application.ViewModels.Response;

namespace Drones.Application.Interfaces
{
    public interface IDroneMedicationApplication
    {
        Task<BaseResponse<bool>> RegisterDrone(DroneRequestViewModel requestViewModel);
        Task<BaseResponse<bool>> LoadDroneWithMeditionItems(DispatchRequestViewModel requestViewModel);
        Task<BaseResponse<List<MedicationResponseViewModel>>> CheckingLoadedMedicationItemsByDroneGiven(int droneId);
        Task<BaseResponse<List<DroneResponseViewModel>>> CheckingAvailableDronesForLoaded();
        Task<BaseResponse<int>> CheckDroneBatteryLevelByDroneGiven(int droneId);
    }
}
