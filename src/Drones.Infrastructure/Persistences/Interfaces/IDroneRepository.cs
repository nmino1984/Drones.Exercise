using Drones.Domain.Entities;

namespace Drones.Infrastructure.Persistences.Interfaces
{
    public interface IDroneRepository : IGenericRepository<TDrone>
    {
        Task<TDrone> GetDroneBySerialNumber(string serialNumber);
        Task<bool> GetIfDroneAvailable(int droneId);
        Task<int> GetDroneBattery(int droneId);
        Task<int> GetDroneWeightLimit(int droneId);
    }
}
