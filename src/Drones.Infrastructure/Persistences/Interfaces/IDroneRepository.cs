using Drones.Domain.Entities;
using Drones.Utilities.Statics;

namespace Drones.Infrastructure.Persistences.Interfaces
{
    public interface IDroneRepository : IGenericRepository<TDrone>
    {
        Task<bool> IsPossibleToAddADrone();
        Task<TDrone> GetDroneBySerialNumber(string serialNumber);
        Task<bool> GetIfDroneAvailable(int droneId);
        Task<double> GetDroneBattery(int droneId);
        Task<bool> ChangeStateToDrone(int droneId, StateTypes newState);
        Task<double> GetDroneWeightLimit(int droneId);
    }
}
