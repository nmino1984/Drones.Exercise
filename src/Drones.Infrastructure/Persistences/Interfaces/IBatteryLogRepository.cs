using Drones.Domain.Entities;

namespace Drones.Infrastructure.Persistences.Interfaces;

public interface IBatteryLogRepository
{
    Task<List<BatteryLog>> GetAllAsync();
    Task<List<BatteryLog>> GetByDroneIdAsync(int droneId);
}
