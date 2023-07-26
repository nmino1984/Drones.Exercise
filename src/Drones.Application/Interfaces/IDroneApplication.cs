using Drones.Application.Commons.Bases;
using Drones.Application.ViewModels.Drone.Request;
using Drones.Application.ViewModels.Drone.Response;
using Drones.Domain.Entities;
using Drones.Infrastructure.Commons.Bases.Response;
using Drones.Utilities.Statics;

namespace Drones.Application.Interfaces
{
    public interface IDroneApplication
    {
        Task<BaseResponse<BaseEntityResponse<DroneResponseViewModel>>> ListDrones();
        Task<BaseResponse<DroneResponseViewModel>> GetDroneById(int id);
        Task<BaseResponse<bool>> RegisterDrone(DroneRequestViewModel requestViewModel);
        Task<BaseResponse<bool>> EditDrone(int droneId, DroneRequestViewModel requestViewModel);
        Task<BaseResponse<bool>> DeleteDrone(int droneId);
        Task<bool> ChangeStateToDrone(int droneId, StateTypes newState);
        Task<bool> ChangeBatteryLevelToDrone(int droneId, double droneBatterylevel);
    }
}
