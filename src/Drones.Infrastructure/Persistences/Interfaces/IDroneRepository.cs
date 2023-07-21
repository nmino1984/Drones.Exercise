using Drones.Domain.Entities;
using Drones.Infrastructure.Persistences.Interfaces;
using Drones.Utilities.Statics;

namespace Drones.Infrastructure.Persistences.Interfaces
{
    public interface IDroneRepository : IGenericRepository<TDrone>
    {
        Task<bool> GetIfDroneAvailable(int droneId);
        Task<int> GetDroneBattery(int droneId);
        Task<int> GetDroneWeightLimit(int droneId);

    }
}
