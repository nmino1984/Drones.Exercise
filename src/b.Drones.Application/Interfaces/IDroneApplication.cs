using Drones.Application.ViewModels.Drone.Request;
using Drones.Application.ViewModels.Drone.Response;
using Drones.Application.Commons.Bases;
using Drones.Infrastructure.Commons.Bases.Request;
using Drones.Infrastructure.Commons.Bases.Response;

namespace Drones.Application.Interfaces
{
    public interface IDroneApplication
    {
        Task<BaseResponse<BaseEntityResponse<DroneMedicationResponseViewModel>>> ListDrones(BaseFiltersRequest filters);
        Task<BaseResponse<IEnumerable<MedicationSelectResponseViewModel>>> ListSelectDrones();
        Task<BaseResponse<DroneMedicationResponseViewModel>> GetDroneById(int id);
        Task<BaseResponse<bool>> RegisterDrone(DroneMedicationRequestViewModel requestViewModel);
        Task<BaseResponse<bool>> EditDrone(int droneId, DroneMedicationRequestViewModel requestViewModel);
        Task<BaseResponse<bool>> DeleteDrone(int id);

    }
}
